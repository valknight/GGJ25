using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Windows.Backgrounds
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    public class BackgroundSetButton: MonoBehaviour
    {
        [SerializeField] private Material BackgroundMaterial;
        private Image PreviewImage;
        [SerializeField] private TMP_Text PreviewText;

        private void Start()
        {
            PreviewImage = GetComponent<Image>();
            PreviewText?.SetText(BackgroundMaterial.name);
            PreviewImage.material = BackgroundMaterial;
            GetComponent<Button>().onClick.AddListener(Clicked);
        }

        private void Clicked()
        {
            BackgroundRenderer.ChangeMat(BackgroundMaterial);
        }
    }
}