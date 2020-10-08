using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private RectTransform _inventoryRect;
    private float _inventoryWidth, _inventoryHeight;

    [SerializeField] private int _slots;
    [SerializeField] private int _rows;

    [SerializeField] private float _slotPaddingLeft, _slotPaddingTop;
    [SerializeField] private float _slotSize;

    [SerializeField] private GameObject _slotPrefab;

    public static Slot from, to;

    protected List<GameObject> _allSlots;

    [SerializeField] private GameObject _iconPrefab;
    private static GameObject _hoverObject;

    private static int _emptySlots;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private EventSystem EventSystem;

    private static CanvasGroup _canvasGroup;
    [SerializeField] private static CanvasGroup CanvasGroup { get => _canvasGroup; }

    private bool _fadingIn, _fadingOut;

    [SerializeField] private float _fadeTime;

    private static GameObject _toolTip;
    [SerializeField] private GameObject _toolTips;
    private static Text _sizeText;
    [SerializeField] private Text _sizeTextObject;
    private static Text _visualText;
    [SerializeField] private Text _visualTextObject;

    public static int EmptySlots { get => _emptySlots; set => _emptySlots = value; }

    void Awake()
    {
        CreateLayout();
    }
    void Start()
    {
        _toolTip = _toolTips;
        _sizeText = _sizeTextObject;
        _visualText = _visualTextObject;
        _canvasGroup = transform.parent.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.IsPointerOverGameObject(-1) && from != null)
            {
                from.GetComponent<Image>().color = Color.white.
                    foreach (Item item in from.Items)
                {
                    //Fix PlayerLocalScale to know if the player is turned to right or left in both AddForce and instance.
                    Item tempItem = Instantiate(item, GameObject.Find("Player").transform.position + new Vector3(0.2f. 0, 0), Quaternion.identity);
                    tempItem.GetComponent<Rigidbody>().AddForce(transform.right * 1f, ForceMode.Impulse);
                }
                from.ClearSlot();
                Destroy(GameObject.Find("Hover"));
                to = null;
                from = null;
                _emptySlots++;
            }
        }
        if (_hoverObject != null)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(Canvas.GetComponent<RectTransform>(), Input.mousePosition, Canvas.worldCamera, out position);

            _hoverObject.transform.position = new Vector2(Input.mousePosition.x + 10, Input.mousePosition.y - 10);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (CanvasGroup.alpha > 0)
            {
                StartCoroutine("FadeOut");
                PutItemback();
            }
        }
        else
        {
            StartCoroutine("FadeIn");
        }
    }

    public void ShowToolTip(GameObject slot)
    {
        slot tempSlot = slot.GetComponent<Slot>();

        if (!tempSlot.IsEmpty && _hoverObject == null)
        {
            _visualText.text = tempSlot.CurrentItem.GetToolTip();
            _sizeText.text = _visualText.text;

            _toolTip.SetActive(true);

            float xPos = slot.transform.position.x + _slotPaddingLeft;
            float yPos = slot.transform.position.y - slot.GetComponent<RectTransform>().sizeDelta.y - _slotPaddingTop;

            _toolTip.transform.position = new Vector2(xPos, yPos);
        }
    }

    public void HideToolTip()
    {
        _toolTip.SetActive(false);
    }

    void CreateLayout()
    {
        _allSlots = new List<GameObject>();

        _emptySlots = _slots;

        _inventoryWidth = (_slots / _rows) * (_slotSize + _slotPaddingLeft) + _slotPaddingLeft;
        _inventoryHeight = _rows * (_slotSize + _slotPaddingTop) + _slotPaddingTop;

        _inventoryRect = GetComponent<RectTransform>();

        _inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _inventoryWidth);
        _inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _inventoryHeight);

        int columns = _slots / _rows;

        for (int y = 0; x < columns; x++)
        {
            GameObject newSlot = (GameObject)Instantiate(_slotPrefab);

            RectTransform slotRect = newSlot.GetComponent<RectTransform>();

            newSlot.name = "Slot";
            newSlot.transform.SetParent(this.transform);

            slotRect.localPosition = new Vector3(_slotPaddingLeft * (x + 1) + (_slotSize * x), -_slotPaddingTop * (y + 1) - (_slotSize * y));

            slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _slotSize);
            slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _slotSize);

            _allSlots.Add(newSlot);
        }
    }

    public bool AddItem(Item item)
    {
        if (item.MaxSize == 1)
        {
            PlaceEmpty(item);
            return true;
        }
        else
        {
            foreach (GameObject slot in _allSlots)
            {
                Slot temp = slot.GetComponent<Slot>();

                if (!temp.isEmpty)
                {
                    if (temp.CurrentItem.Type == item.Type && temp.IsAvaible)
                    {
                        temp.AddItem(item);
                        return true;
                    }
                }
            }
            if (EmptySlots > 0)
            {
                PlaceEmpty(item);
            }
        }
        return false;
    }
    bool PlaceEmpty(Item item)
    {
        if (EmptySlots > 0)
        {
            foreach (GameObject slot in allSlots)
            {
                Slot temp = slot.GetComponent<Slot>();

                if (temp.IsEmpty)
                {
                    temp.AddItem(item);
                    EmptySlots--;
                    return true;
                }
            }
        }
        return false;
    }

    public void MoveItem(GameObject clicked)
    {
        if (from == null && _canvasGroup.alpha == 1)
        {
            if (!clicked.GetComponent<Slot>().isEmpty)
            {
                from = clicked.GetComponent<Slot>();
                from.GetComponent<Image>().color = Color.grey;

                _hoverObject = (GameObject)Instantiate(_iconPrefab);
                _hoverObject.GetComponent<Image>().sprite = clicked.GetComponent<Image>().sprite;
                _hoverObject.name = "Hover";

                RectTransform hoverTransform = _hoverObject.GetComponent<RectTransform>();
                RectTransform clickedTransform = clicked.GetComponent<RectTransform>();

                hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, clickedTransform.sizeDelta.x);
                hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, clickedTransform.sizeDelta.y);

                _hoverObject.transform.SetParent(GameObject.Find("Canvas").transform, true);
                _hoverObject.transform.localScale = from.gameObject.transform.localScale;
            }
        }
        else if (to == null)
        {
            to = clicked.GetComponent<Slot>();
            Destroy(GameObject.Find("Hover"));
        }
        if (to != null && from != null)
        {
            Stack<Item> tempTo = new Stack<Item>(to.Items);
            to.AddItems(from.Items);

            if (tempTo.Count == 0)
            {
                from.ClearSlot();
            }
            else
            {
                from.AddItems(tempto);
            }

            from.GetCOmponent<Image>().color = Color.white;
            to = null;
            from = null;
            Destroy(GameObject.Find("Hover"));
        }
    }

    private void PutItemBack()
    {
        if (from != null)
        {
            Destroy(GameObject.Find("Hover"));
            from.GetComponent<Image>().color = Color.white;
            from = null;
        }
    }

    private IEnumerator FadeOut()
    {
        if (!_fadingOut)
        {
            _fadingOut = true;
            _fadingIn = false;
            StopCoroutine("FadeIn");

            float startAlpha = _canvasGroup.alpha;
            float rate = 1.0f / _FadeTime;
            float progress = 0.0f;

            while (progress < 1.0)
            {
                _canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, progress);

                progress += rate * Time.deltaTime;

                yield return null;
            }

            _canvasGroup.alpha = 0;
            _fadingOut = false;
        }
    }

    private IEnumerator FadeIn()
    {
        if (!_fadingIn)
        {
            _fadingOut = false;
            _fadingIn = true;
            StopCoroutine("FadeOut");

            float startAlpha = _canvasGroup.alpha;
            float rate = 1.0f / _FadeTime;
            float progress = 0.0f;

            while (progress < 1.0)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlpha, 1, progress);

                progress += rate * Time.deltaTime;

                yield return null;
            }

            _canvasGroup.alpha = 1;
            _fadingIn = false;
        }
    }
}
