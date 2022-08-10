using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField] private AudioSource ChangeColorAudio;
    [SerializeField] private AudioSource BounceAudio;
    public void ChangeColor(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", Random.ColorHSV());

            ChangeColorAudio.Play();
        }
    }


    public void Bounce(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            BounceAudio.Play();
            // Example of DOTween with coroutine - useful for complicated sequences
            StartCoroutine(BounceSequence());
        }
    }

    private IEnumerator BounceSequence()
    {
        Sequence openingSequence = DOTween.Sequence();
        var movementTween= transform.DOPunchScale(new Vector3(1, 1, 1), 1, 3).SetEase(Ease.InOutSine);
        openingSequence.Join(movementTween);
        while (openingSequence.IsActive()) { yield return null; }
    }
}