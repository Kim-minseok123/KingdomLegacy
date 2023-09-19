using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using System;

public class UI_BattlePopup : UI_Popup
{
    enum GameObjects
    {
        PlayerTransform,
        FriendTransform1,
        FriendTransform2,
        FriendTransform3,
        FriendTransform4,
        FriendTransform5,
        FriendTransform6,
        EnemyTransform1,
        EnemyTransform2,
        EnemyTransform3,
        EnemyTransform4,
        CardList,
        UseTarget,
        ArrowController,
    }
    enum Buttons
    {
        DrawDeckCard,
        ThrowDeckCard,
        ExitDeckCard,
        TurnEndButton
    }
    enum Texts
    {
        DrawCardText,
        ThrowCardText,
        ExitCardText,
        ManaText,
    }
    enum Images
    {
        BattleGroundImage,
    }
    enum States
    {
        BattleStart, 
        TurnStart,
        Turning,
        TurnEnd,
        BattleEnd,
    }

    //뽑을 카드 더미, 손패 카드, 버린 카드 더미, 제외된 카드 더미
    List<CardData> _drawCards = new();
    List<UI_Card> _handCardsUI = new();
    List<CardData> _throwCards = new();
    List<CardData> _exitCards = new();

    //카드를 회전하기 위한 정보
    [SerializeField][Range(0, 5)] private float selectionSpacing = 1;
    [SerializeField] private Vector3 curveStart = new Vector3(-500f, -500f, 0);
    [SerializeField] private Vector3 curveMiddle = new Vector3(0, -350f, 0);
    [SerializeField] private Vector3 curveEnd = new Vector3(500f, -500f, 0);

    private float _selectUp = -3f;

    private Vector3 _a, _b, _c; // 카드를 회전하는데 사용할 베지어 곡선

    private int _selected = -1; // 마우스로 선택된 카드 인덱스
    private UI_Card _selectCard = null;

    //현재 스테이지
    private int _stage = 0;
    public int _curTurn = 0;
    public int _curMana = 0;
    public int _maxMana = 0;


    //턴 시작 시 드로우 카드 수
    int _startDrawCardNum = 5;

    //상태 관료 플러그
    States _state = States.TurnStart;
    bool isDraggingCard = false;
    bool isUseCard = false;
    bool cardsDrawn = false;

    //플레이어
    GameObject _player;
    Animator _playerAnim;
    EnemyInfo _enemyInfo;
    List<GameObject> _enemyList = new();
    ArrowController _ar;
    [SerializeField] private Ease ease;
    public override bool Init()
    {
        Camera.main.orthographicSize = 5;

        Canvas canvas = Utils.GetOrAddComponent<Canvas>(this.gameObject);
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;
        transform.localScale = new Vector3(.00925f, .00925f, 00925f);
        _a = transform.TransformPoint(curveStart);
        _b = transform.TransformPoint(curveMiddle);
        _c = transform.TransformPoint(curveEnd);

        BindObject(typeof(GameObjects));
        BindImage(typeof(Images));
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));

        _player = Managers.Resource.Instantiate("PlayerCharacter/" + Managers.Game.PlayerName, GetObject((int)GameObjects.PlayerTransform).transform);
        _playerAnim = _player.GetComponent<Animator>();

        //적들 소환
        for (int i = 0; i < _enemyInfo.Enemys.Count; i++) {
            var enemy = Managers.Resource.Instantiate(_enemyInfo.Enemys[i], GetObject(i + 7).transform);
            enemy.GetComponent<EnemyController>().Setting(_enemyInfo.EnemyHp[i]);
            _enemyList.Add(enemy);
        }

        GetButton((int)Buttons.TurnEndButton).gameObject.BindEvent(TurnEnd);
        _ar = GetObject((int)GameObjects.ArrowController).GetComponent<ArrowController>(); 
        RefreshUI();

        return true;
    }
    void RefreshUI()
    {
        GetText((int)Texts.DrawCardText).text = _drawCards.Count.ToString();
        GetText((int)Texts.ThrowCardText).text = _throwCards.Count.ToString();
        GetText((int)Texts.ExitCardText).text = _exitCards.Count.ToString();
        GetText((int)Texts.ManaText).text = _curMana.ToString() + " / " + _maxMana.ToString();
    }
    public void SetInfo(EnemyInfo enemyInfo)
    {
        //스테이지, 체력, 카드 등 값 넘겨줘야함
        _maxMana = Managers.Game.Mana;
        _stage = Managers.Game.Stage;
        _drawCards.Clear();
        _drawCards = Managers.Game.Cards;
        _startDrawCardNum = Managers.Game.StartDrawCardNum;
        _drawCards.Shuffle();
        _handCardsUI.Clear();
        _throwCards.Clear();
        _exitCards.Clear();
        _enemyInfo = enemyInfo;

        _state = States.BattleStart;
    }

    private void Update()
    {
        switch (_state)
        {
            case States.BattleStart:
                GameEvents.OnBattleStart();
                _state = States.TurnStart;
                break;
            case States.TurnStart:
                GameEvents.OnTurnStart();
                _curTurn++;
                if (!cardsDrawn)
                {
                    DrawCards(_startDrawCardNum);
                    cardsDrawn = true;
                    HealMana(_maxMana);
                }
                _state = States.Turning;
                break;
            case States.Turning:
                if (!isUseCard)
                {
                    CheckCards();
                    HandleCardsCircle();
                }
                break;
            case States.TurnEnd:
                if (!isDestoryCard)
                {
                    HandleCardsCircle();
                }
                // 상대 턴 시작
                break;
            case States.BattleEnd:
                break;
        }
    }

    public void DrawCards(int drawcardsnum)
    {
        StartCoroutine(Draw(drawcardsnum));
    }
    public IEnumerator Draw(int drawcardsnum) {
        if (cardsDrawn)
            GameEvents.OnDrawCard();
        for (int i = 0; i < drawcardsnum; i++)
        {
            if (_drawCards.Count <= 0)
            {
                _drawCards.AddRange(_throwCards);
                _throwCards.Clear();
                _drawCards.Shuffle();
                GameEvents.OnShuffleDeck();
            }
            if ((_drawCards.Count <= 0 && _throwCards.Count <= 0) || _handCardsUI.Count >= 10)
            {
                Debug.Log("더이상 카드를 드로우 할 수 없습니다.");
                yield break;
            }
            CardData cardData = _drawCards[0];
            _drawCards.RemoveAt(0);
            DrawCards(cardData);
            yield return new WaitForSeconds(0.25f);
        }
    }
    public void DrawCards(CardData cardData) {
        var NewCardUI = Managers.UI.MakeSubItem<UI_Card>(GetObject((int)GameObjects.CardList).transform).SetInfo(cardData.ID);
        NewCardUI.gameObject.BindEvent((obj) => PointEnterSelectCard(obj), Define.UIEvent.PointerEnter);
        NewCardUI.gameObject.BindEvent(PointExitCard, Define.UIEvent.PointerExit);
        NewCardUI.gameObject.BindEvent((obj) => DragCard(obj), Define.UIEvent.Drag);
        NewCardUI.gameObject.BindEvent((obj) => EndDragCard(obj), Define.UIEvent.DragEnd);
        _handCardsUI.Add(NewCardUI);
        RefreshUI();
    }
    private void HandleCardsCircle()
    {
        int count = _handCardsUI.Count;
        float baseT = 0.5f;
        float spacing = 1.0f / Mathf.Max(6, count + 1);

        for (int i = 0; i < count; i++)
        {
            if (_handCardsUI[i] == null)
                continue;
            var card = _handCardsUI[i];
            if (card == _selectCard) _selected = i;
            var cardTransform = card.transform;

            float selectOffset = 0.02f *
                Mathf.Clamp01(1 - Mathf.Abs(Mathf.Abs(i - _selected) - 1) / (float)count * 3) *
                                   Mathf.Sign(i - _selected);

            var t = baseT + (i - count * 0.5f + 0.5f) * spacing + selectOffset * selectionSpacing;
            var targetPosition = Utils.GetCurvePoint(_a, _b, _c, t);
            var cardUp = Utils.GetCurveNormal(_a, _b, _c, t) * -1;
            var cardForward = Vector3.forward;

            if (_selected != i && !isDraggingCard)
            {
                cardTransform.rotation = Quaternion.Slerp(cardTransform.rotation,
                    Quaternion.LookRotation(cardForward, cardUp), 0.1f);

                card.transform.SetSiblingIndex(i);
                cardTransform.position = Vector3.Lerp(cardTransform.position, targetPosition, 0.1f);
            }
            else if (_selected == i && !isDraggingCard)
            {
                cardTransform.rotation = Quaternion.Slerp(cardTransform.rotation,
                    Quaternion.LookRotation(cardForward), 0.1f);

                card.transform.SetSiblingIndex(99);
                var selectedPosition = new Vector3(targetPosition.x, _selectUp, targetPosition.z);
                cardTransform.position = Vector3.Lerp(cardTransform.position, selectedPosition, 0.2f);
            }

            // 카드 흔들림 방지
            if (Vector3.Distance(cardTransform.position, targetPosition) < 0.01f)
            {
                cardTransform.position = targetPosition;
            }
        }
    }
    public void HealMana(int value) {
        _curMana += value;
        RefreshUI();
    }
    bool isDestoryCard = false;
    public void TurnEnd()
    {
        if (_state == States.Turning && !isUseCard)
        {
            _state = States.TurnEnd;
            GameEvents.OnTurnEnd();
            isDestoryCard = true;
            _curMana = 0;
            StartCoroutine(CheckDestoryCard());
            
        }

    }
    IEnumerator CheckDestoryCard() {
        if (!Managers.Game.isPreservation)
            yield return StartCoroutine(DestroyCardRoutine());
        RefreshUI();
        isDestoryCard = false;

        //임시 차례
        cardsDrawn = false;
        _state = States.TurnStart;
    }

    IEnumerator DestroyCardRoutine()
    {
        Vector3 target = GetButton((int)Buttons.ThrowDeckCard).gameObject.transform.position;

        List<Tweener> tweeners = new List<Tweener>();

        for (int i = 0; i < _handCardsUI.Count; i++)
        {
            if (_handCardsUI[i]._cardData.state == Define.CardLifeState.Preservation)
            {
                continue;
            }

            var cardTransform = _handCardsUI[i].transform;
            var currentIndex = i;

            // Movement Tween
            Tweener moveTweener = cardTransform.DOMove(target, 0.7f)
                .SetEase(Ease.OutQuart);

            // Scale Down Effect
            Tweener scaleTweener = cardTransform.DOScale(Vector3.zero, 0.7f)
                .SetEase(Ease.OutQuart)
                .OnComplete(() =>
                {
                    _throwCards.Add(_handCardsUI[currentIndex]._cardData);
                    Managers.Resource.Destroy(_handCardsUI[currentIndex].gameObject);
                    _handCardsUI[currentIndex] = null;
                });

            tweeners.Add(moveTweener);
            tweeners.Add(scaleTweener);
        }

        while (tweeners.Count > 0)
        {
            tweeners.RemoveAll(t => !t.IsActive() || t.IsComplete());
            yield return null;
        }

        _handCardsUI.RemoveAll(ui => ui == null);
    }

    public void PointEnterSelectCard(GameObject obj)
    {
        var card = obj.GetComponent<UI_Card>();
        if ((_state == States.Turning || isUseCard) && card._isUseCard)
            _selectCard = card;
    }
    public void PointExitCard()
    {
        if (_state == States.Turning || isUseCard)
        {
            _selectCard = null;
            _selected = -1;
        }
    }
    bool _isDragArrow = false;
    public void DragCard(GameObject obj)
    {
        var card = obj.GetComponent<UI_Card>();
        if (_state != States.Turning || isUseCard || !card._isUseCard)
            return;
        isDraggingCard = true;
        Vector3 screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(obj.transform.position).z);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPoint);

        if (card._cardData.target != Define.TargetType.Enemy)
        {
            obj.transform.SetPositionAndRotation(worldPos, Quaternion.identity);
        }
        else
        {
            if (worldPos.y > -0.8f || _isDragArrow)
            {
                Vector3 target = GetObject((int)GameObjects.UseTarget).transform.position;
                obj.transform.DOMove(target, 0.5f).SetEase(ease);
                obj.transform.rotation = Quaternion.identity;
                //화살표 그리기.
               _ar.DrawArrow(obj.transform.position, worldPos);
                _isDragArrow = true;
            }
        }
    }
    public void EndDragCard(GameObject obj)
    {
        var card = obj.GetComponent<UI_Card>();
        if (_state != States.Turning || isUseCard || !card._isUseCard)
            return;
        isDraggingCard = false;
        Vector3 screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(obj.transform.position).z);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPoint);
        if (card._cardData.target != Define.TargetType.Enemy)
        {
            if (worldPos.y > -0.8f)
            {
                //카드 사용.
                isUseCard = true;
                StartCoroutine(UseCardNonTarget(card));
            }
        }
        else {
            //타겟이 있는 경우만
            _ar.StopDrawArrow();
            _isDragArrow = false;
        }
    }

    IEnumerator UseCardNonTarget(UI_Card obj)
    {
        obj.transform.DOMove(transform.position, 0.5f).SetEase(ease);
        //스킬 사용.
        _playerAnim.SetTrigger("Attack");

        _curMana -= obj._cardData.mana;
        yield return new WaitForSeconds(0.7f);
        
        //
        Vector3 target = GetButton((int)Buttons.ThrowDeckCard).gameObject.transform.position;

        obj.transform.DOMove(target, 0.5f).SetEase(ease);
        obj.transform.DOScale(Vector3.zero, 0.5f).SetEase(ease);
        yield return new WaitForSeconds(0.5f);

        int i = _handCardsUI.IndexOf(obj);
        if (i != -1)
        {
            if (obj._cardData.state == Define.CardLifeState.Extinction)
            {
                _exitCards.Add(_handCardsUI[i]._cardData);
                GameEvents.OnExitCard();
            }
            else
                _throwCards.Add(_handCardsUI[i]._cardData);
            Managers.Resource.Destroy(_handCardsUI[i].gameObject);
            _handCardsUI[i] = null;
        }

        _handCardsUI.RemoveAll(ui => ui == null);
        RefreshUI();
        isUseCard = false;
    }
    IEnumerator UseCardTarget(UI_Card obj) {
        _playerAnim.SetTrigger("Attack");
        _curMana -= obj._cardData.mana;
        yield return null;
    }
    public void CheckCards() {
        for (int i = 0; i < _handCardsUI.Count; i++) {
            _handCardsUI[i].CheckManaUseCard(_curMana);
        }
    }
}
