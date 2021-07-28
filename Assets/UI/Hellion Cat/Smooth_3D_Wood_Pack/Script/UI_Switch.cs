using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HellionCat.Wood_Pack_UI
{
    /// <summary>
    /// Script to make a switch in UI
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class UI_Switch : MonoBehaviour
    {
        /// <summary>
        /// Image used to display the sprites
        /// </summary>
        [SerializeField, HideInInspector]
        Image m_image;
        /// <summary>
        /// Sprite when the switch is on
        /// </summary>
        [SerializeField]
        [Tooltip("Sprite when the switch is on")]
        Sprite m_onSprite;
        /// <summary>
        /// Sprite when the switch is off
        /// </summary>
        [SerializeField]
        [Tooltip("Sprite when the switch is off")]
        Sprite m_offSprite;
        /// <summary>
        /// Determine if the switch is on or off
        /// </summary>
        [Tooltip("Determine if the switch is on or off")]
        [SerializeField]
        bool m_isOn;

        /// <summary>
        /// Determine if the switch is on or off
        /// </summary>
        public bool IsOn => m_isOn;

        /// <summary>
        /// Change the phase of the switch
        /// </summary>
        public void Switch()
        {
            Switch(!IsOn);
        }

        /// <summary>
        /// Change the phase of the switch by p_setOn
        /// </summary>
        /// <param name="p_setOn">the new value of the switch</param>
        public void Switch(bool p_setOn)
        {
            m_isOn = p_setOn;
            m_image.sprite = (m_isOn) ? m_onSprite : m_offSprite;
        }

#if UNITY_EDITOR
        bool m_oldisOn;
        private void OnValidate()
        {
            if (!m_image)
                m_image = GetComponent<Image>();
            if (!m_image.sprite)
            {
                if (m_onSprite)
                    Switch(true);
                else if (m_offSprite)
                    Switch(false);
            }

            if (m_oldisOn != m_isOn)
            {
                m_oldisOn = m_isOn;
                Switch(m_isOn);
            }
        }
#endif
    }
}