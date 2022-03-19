using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CardBattle.Core
{
    public class Slider : MonoBehaviour
    {
        [SerializeField] private Button left;
        [SerializeField] private Button right;
        [SerializeField] private Transform textArea;
        [SerializeField] private int current;
        [SerializeField] private bool updateAtStart = true;

        [SerializeField] private string[] items;

        // Start is called before the first frame update
        void Start()
        {
            left.onClick.AddListener(() => ButtonClick(-1));
            right.onClick.AddListener(() => ButtonClick(1));
            if (updateAtStart) ButtonClick(0);
        }

        private void ButtonClick(int lr)
        {
            switch (lr)
            {
                case -1:
                    current -= (current - 1 < 0) ? 0 : 1;
                    break;
                case 1:
                    current += (current + 1 >= items.Length) ? 0 : 1;
                    break;
                default:
                    break;
            }

            textArea.Find("Placeholder").gameObject.SetActive(false);
            textArea.Find("Text").GetComponent<TMP_Text>().SetText(items[current]);
        }

        /// <summary>
        /// Converts the current index to an enum value
        /// </summary>
        /// <typeparam name="T">The specific enum</typeparam>
        /// <returns>returns the current value in form of a specific enum</returns>
        /// <exception cref="Exception"></exception>
        public T GetCurrent<T>() where T : struct, IConvertible
        {
            if (typeof(T).IsEnum)
            {
                return (T)Enum.Parse(typeof(T), current.ToString());
            }
            else throw new Exception("Has to be an enum type!");
        }
    }
}
