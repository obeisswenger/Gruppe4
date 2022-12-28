    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class FPSCounter : MonoBehaviour
    {
        public float timer, refresh, avgFramerate;
        public Text m_Text;
        public TextMeshProUGUI textMeshProUGUI;

        private void Start()
        {
            m_Text = GetComponent<Text>();
        }
        private void Update()
        {
            //Change smoothDeltaTime to deltaTime or fixedDeltaTime to see the difference
            float timelapse = Time.smoothDeltaTime;
            timer = timer <= 0 ? refresh : timer -= timelapse;
            if(timer <= 0) avgFramerate = (int) (1f / timelapse);
            textMeshProUGUI.text = avgFramerate.ToString();
        }
    }
