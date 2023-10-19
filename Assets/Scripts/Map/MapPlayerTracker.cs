using System;
using System.Collections;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Map
{
    public class MapPlayerTracker : MonoBehaviour
    {
        public bool lockAfterSelecting = false;
        public bool isAnimWork = false;
        public float enterNodeDelay = 1f;
        public MapManager mapManager;
        public MapView view;

        public static MapPlayerTracker Instance;

        public bool Locked { get; set; }

        private void Awake()
        {
            Instance = this;
        }

        public void SelectNode(MapNode mapNode)
        {
            if (Locked) return;

            // Debug.Log("Selected node: " + mapNode.Node.point);

            if (mapManager.CurrentMap.path.Count == 0)
            {
                // player has not selected the node yet, he can select any of the nodes with y = 0
                if (mapNode.Node.point.y == 0)
                    SendPlayerToNode(mapNode);
                else
                    PlayWarningThatNodeCannotBeAccessed();
            }
            else
            {
                var currentPoint = mapManager.CurrentMap.path[mapManager.CurrentMap.path.Count - 1];
                var currentNode = mapManager.CurrentMap.GetNode(currentPoint);

                if (currentNode != null && currentNode.outgoing.Any(point => point.Equals(mapNode.Node.point)))
                    SendPlayerToNode(mapNode);
                else
                    PlayWarningThatNodeCannotBeAccessed();
            }
        }

        private void SendPlayerToNode(MapNode mapNode)
        {
            Locked = lockAfterSelecting;
            mapManager.CurrentMap.path.Add(mapNode.Node.point);
            //mapManager.SaveMap();
            view.SetAttainableNodes();
            view.SetLineColors();
            mapNode.ShowSwirlAnimation();
            isAnimWork = true;
            DOTween.Sequence().AppendInterval(enterNodeDelay).OnComplete(() => { EnterNode(mapNode); StartCoroutine(AnimWorkWating()); });
        }
        IEnumerator AnimWorkWating() {
            yield return new WaitForSeconds(1f);
            isAnimWork = false;
        }
        private static void EnterNode(MapNode mapNode)
        {
            Managers.Game.StageNumber++;
            // we have access to blueprint name here as well
            Debug.Log("Entering node: " + mapNode.Node.blueprintName + " of type: " + mapNode.Node.nodeType);
            // load appropriate scene with context based on nodeType:
            // or show appropriate GUI over the map: 
            // if you choose to show GUI in some of these cases, do not forget to set "Locked" in MapPlayerTracker back to false
            EnemyInfo info;
            switch (mapNode.Node.nodeType)
            {
                case NodeType.MinorEnemy:
                    info = Managers.Resource.Load<EnemyInfo>($"ScriptableObjects/Enemy/Minor/{Managers.Game.Stage}-{Managers.Game.StageNumber}");
                    Managers.UI.ShowPopupUI<UI_BattlePopup>().SetInfo(info);
                    break;
                case NodeType.EliteEnemy:
                    info = Managers.Resource.Load<EnemyInfo>($"ScriptableObjects/Enemy/Elite/{Managers.Game.Stage}-{Managers.Game.StageNumber}");
                    Managers.UI.ShowPopupUI<UI_BattlePopup>().SetInfo(info);
                    break;
                case NodeType.RestSite:
                    break;
                case NodeType.Treasure:
                    break;
                case NodeType.Boss:
                    info = Managers.Resource.Load<EnemyInfo>($"ScriptableObjects/Enemy/Boss/{Managers.Game.Stage}-{Managers.Game.StageNumber}");
                    Managers.UI.ShowPopupUI<UI_BattlePopup>().SetInfo(info);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            Managers.Game.CurMapNode = mapNode;
        }

        private void PlayWarningThatNodeCannotBeAccessed()
        {
            Debug.Log("Selected node cannot be accessed");
        }
    }
}