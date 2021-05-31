using TMPro;
using UnityEngine;

namespace UI
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField] private Color _color;
        [SerializeField] private float _timeUntilDissapear;
        [SerializeField] private float _speed;
        private float _timeAlive;
    
        private float _alpha;
        private TextMeshPro _mesh;
        private Renderer _renderer;
        void Awake()
        {
            _mesh = GetComponent<TextMeshPro>();
            _mesh.color = _color;
            _mesh.fontSize = 8;
            _mesh.rectTransform.pivot = new Vector2(0.5f, 0);
            _mesh.autoSizeTextContainer = true;
            _renderer = GetComponent<Renderer>();
            _renderer.sortingLayerName = "Foreground";
            _renderer.sortingOrder = 0;
        
            _alpha = 1;
        }

        public void SetText(string text)
        {
            _mesh.text = text;
        }

        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }
    
        void Update()
        {
            _timeAlive += Time.deltaTime;
            _alpha = Mathf.SmoothStep(2, 0, _timeAlive / _timeUntilDissapear);
        
            _mesh.color = new Color(_color.a, _color.g, _color.b, _alpha);
        
            transform.Translate(Vector2.up * (_speed * Time.deltaTime));

            if (_timeAlive >= _timeUntilDissapear)
            {
                Destroy(gameObject);
            }
        }
    }
}
