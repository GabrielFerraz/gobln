using System;
using System.Collections;
using System.Collections.Generic;
using _Script.Oponents;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class SoloDistraction : MonoBehaviour
{
    public GoblinBehaviour[] goblins;
    public float distractionDuration = 5f;
    public float cooldownDuration = 15f;
    public float cancelCooldownDuration = 1f;
    public float imageDuration = 1f;
    public SpriteRenderer imageObject;
    public Sprite distractionSprite;

    private float _currentCooldown = 0f;
    private float _imageTimer = 0f;
    private bool _active = false;
    private bool _onCooldown = false;
    private SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
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
    }
    // Start is called before the first frame update

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

    private void ToggleAllAvailable()
    {
        foreach (var goblin in goblins)
        {
            if (!goblin.IsDistracted())
            {
                goblin.ToggleAvailableToDistract();
            }
        }
    }

    private IEnumerator ShowImage(GoblinBehaviour g)
    {
        imageObject.sprite = distractionSprite;
        imageObject.gameObject.SetActive(true);
        yield return new WaitForSeconds(imageDuration);
        imageObject.gameObject.SetActive(false);
        g.Distract(distractionDuration);
        _onCooldown = true;
        _currentCooldown = cooldownDuration;
        Color c = _renderer.color;
        c.a = 0.1f;
        _renderer.color = c;
    }
}
