using System.Collections;
using System.Collections.Generic;
using Humanoid;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

public class PlayerInfoUI : MonoBehaviour
{
    [Inject] private IPlayerWeaponValues playerWeaponValues;
    [SerializeField] private TextMeshProUGUI ammoText;
    
    void Start()
    {
        playerWeaponValues.AmmoCount
            .Subscribe(ctx => ammoText.text = ctx.ToString())
            .AddTo(this);
    }
}
