using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator), typeof(Image))]
public class BonusTakeOverlay : MonoBehaviour
{
    private const string ANIM_ON_BONUS_TAKEN = "BonusTakeOverlay_OnTake";

    private Animator _animator;
    private Image _image;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _image = GetComponent<Image>();
    }

    public void OnBonusTaken(Bonus bonus)
    {
        var color = bonus.Color;

        _image.color = new Color(color.r, color.g, color.b, _image.color.a);

        _animator.Play(ANIM_ON_BONUS_TAKEN, 0, 0);
    }
}