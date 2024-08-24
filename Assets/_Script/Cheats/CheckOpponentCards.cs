using System.Collections;
using System.Collections.Generic;
using _Script.Oponents;
using UnityEngine;

public class CheckOponentCards : MonoBehaviour
{
    [SerializeField] private GoblinBehaviour[] goblins;
    [SerializeField] private GameObject cardsGameObject;
    [SerializeField] private SpriteRenderer imageObject;
    [SerializeField] private Sprite distractionSprite;
    [SerializeField] private float imageDuration = 1f;
    [SerializeField] private float cooldownDuration = 15f;
    [SerializeField] private float cardsDuration = 3f;
    [SerializeField] private int distrustLvl = 1;
    private bool _onCooldown = false;
    private float _currentCooldown = 0f;
    private float _cardsTimer = 0f;
    private float _imageTimer = 0f;
    private bool _active = false;
    private SpriteRenderer _renderer;
    // Start is called before the first frame update
    void Start()
    {
        _renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_active)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                foreach (var goblin in goblins)
                {
                    if (hit.collider == goblin.GetComponent<Collider2D>())
                    {
                        Debug.Log("Collided");
                        _active = false;
                        ToggleAllAvailable();
                        goblin.AddDistrust(distrustLvl);
                        StartCoroutine(ShowImage(goblin));
                    }
                }
            }
        }

        if (_onCooldown && _currentCooldown > 0)
        {
            _currentCooldown -= Time.deltaTime;
        }
        else
        {
            _onCooldown = false;
            Color c = _renderer.color;
            c.a = 1f;
            _renderer.color = c;
        }

        if (_cardsTimer > 0)
        {
            _cardsTimer -= Time.deltaTime;
        }
        else
        {
            cardsGameObject.SetActive(false);
        }
    }

    private IEnumerator ShowImage(GoblinBehaviour g)
    {
        imageObject.sprite = distractionSprite;
        imageObject.gameObject.SetActive(true);
        yield return new WaitForSeconds(imageDuration);
        imageObject.gameObject.SetActive(false);
        RevealCards(g.GetComponent<Character>().cardsHand);
        _onCooldown = true;
        _currentCooldown = cooldownDuration;
        Color c = _renderer.color;
        c.a = 0.1f;
        _renderer.color = c;
    }

    private void ToggleAllAvailable()
    {
        foreach (var goblin in goblins)
        {
            goblin.ToggleAvailableToCheat();
        }
    }

    private void OnMouseDown()
    {
        if (_onCooldown) return;


        _active = !_active;
        ToggleAllAvailable();
        // Small cooldown if cancel the action
        if (_active == false)
        {
            _onCooldown = true;
            _currentCooldown = 1f;
            Color c = _renderer.color;
            c.a = 0.1f;
            _renderer.color = c;
        }
    }

    private void RevealCards(Cards[] cards)
    {
        SpriteRenderer[] renderers = cardsGameObject.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            var r = renderers[i];
            var card = cards[i];
            Sprite s = Resources.Load<Sprite>(card.cardSpritePath);
            r.sprite = s;
        }
        cardsGameObject.SetActive(true);
        _cardsTimer = cardsDuration;
    }
}
