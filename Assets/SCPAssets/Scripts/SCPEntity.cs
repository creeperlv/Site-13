using Site13ExternalKernel.Difficulty;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{

    public class SCPEntity : MonoBehaviour
    {
        public bool isPlayer = false;
        public bool Immortal= false;
        public bool DestroyWhenDead = true;
        public bool applyScaleToReplacement = true;
        public int DoorAutomationLevel = 0;
        //[HideInInspector]
        public float CurrentHealth;
        public float MaxHealth;
        public GameObject DeathReplacement;
        [Tooltip("Assign this, the attached game object will be destoryed/deactivated.")]
        public GameObject AttachedGameObject;
        public GameObject DamageEffect;
        public bool generateReplacementOnAttachedGameObject = false;
        public Vector3 ScaleOverride;
        public EntiryGroup EntiryGroup = EntiryGroup.Generic;
        [Header("Settings for difficulty.")]
        public string EntityID;
        void Start()
        {
            if (EntityID != "")
            {
                //DifficultyManager.CurrentDefinition.RawDefinition.Contain
                //ID: HP:<EntityID>
                MaxHealth=float.Parse(DifficultyManager.CurrentDefinition.Get("HP:"+EntityID,0, MaxHealth+""));
            }
            CurrentHealth = MaxHealth;
            if (isPlayer)
            {
                try
                {
                    if (GameInfo.CurrentGame.saveControlProtocolMode == GameInfo.SaveControlProtocolMode.Load)
                        CurrentHealth = GameInfo.CurrentGame.PlayerHealth.CurrentHealth;
                }
                catch (System.Exception)
                {
                }
                GameInfo.CurrentGame.PlayerHealth = this;
            }
        }
        public void ChangeHealth(float Delta)
        {
            if (Immortal == false)
            {
                CurrentHealth += Delta;
                if (CurrentHealth > MaxHealth)
                {
                    CurrentHealth = MaxHealth;
                }
                else if (CurrentHealth < 0)
                {
                    if(CurrentHealth != float.MinValue)
                    CurrentHealth = 0;
                }
                if (CurrentHealth == 0)
                {
                    Die();
                    CurrentHealth = float.MinValue;
                }
            }
        }
        public void Die()
        {
            try
            {

                if (DeathReplacement != null)
                {
                    GameObject go;
                    if (generateReplacementOnAttachedGameObject == false)
                    {

                        go = Instantiate(DeathReplacement, transform.position, transform.rotation);
                        go.transform.position = transform.position;
                        if(applyScaleToReplacement)
                        go.transform.localScale = this.transform.localScale;
                    }
                    else
                    {

                        go = Instantiate(DeathReplacement, AttachedGameObject.transform.position, AttachedGameObject.transform.rotation);
                        go.transform.position = AttachedGameObject.transform.position;
                        if (applyScaleToReplacement)
                            go.transform.localScale = AttachedGameObject.transform.localScale;
                    }
                    if (ScaleOverride != null && ScaleOverride != new Vector3())
                    {
                        if (applyScaleToReplacement)
                            go.transform.localScale = ScaleOverride;
                    }
                }

                if (isPlayer)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                if (DestroyWhenDead == true)
                {
                    if (AttachedGameObject != null) Destroy(AttachedGameObject);
                    Destroy(gameObject);
                }
                else
                {
                    if (AttachedGameObject != null) AttachedGameObject.SetActive(false); gameObject.SetActive(false);
                }
            }
            catch (System.Exception)
            {
            }
        }
    }
    public enum EntiryGroup
    {
        SCP,Human_GOC,Human_Foundation,Generic
    }
}