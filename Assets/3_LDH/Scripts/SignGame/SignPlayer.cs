using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace MiniGame
{
    public class SignPlayer : MonoBehaviour, IPlayer
    {
        [Header("게임 오브젝트")]
        [SerializeField] private GameObject leftHand;
        [SerializeField] private GameObject rightHand;
        

        public Dictionary<Define.PlayerAction, Action> GetKeyDownActionMap()
        {
            return new()
            {
                { Define.PlayerAction.GetPaper, GetPaper },
                { Define.PlayerAction.Sign, Sign }
            };
        }

        public Dictionary<Define.PlayerAction, Action> GetKeyUpActionMap()
        {
            return new();
        }


        #region Actions

        public void GetPaper()
        {
            Debug.Log("종이 가져오기!");
            leftHand.GetComponent<IExecutable>().Execute();
            SoundManager.Instance.PlaySFX(ESFXs.PaperSFX);

        }
        
        public void Sign()
        {
            Debug.Log("싸인하기!");
            rightHand.GetComponent<IExecutable>().Execute();
            SoundManager.Instance.PlaySFX(ESFXs.SignSFX);
        }
        

        #endregion

    }
}