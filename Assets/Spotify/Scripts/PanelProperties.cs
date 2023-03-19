using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Rendering.Universal;
using static UnityEngine.GraphicsBuffer;

namespace Oculus.Interaction
{
    [ExecuteAlways]
    public class PanelProperties : MonoBehaviour
    {
        [SerializeField]
        private MaterialPropertyBlockEditor _editor;

        [SerializeField]
        private FingerPosition _fingerPosition;

        [SerializeField]
        private float _width = 1.0f;

        [SerializeField]
        private float _height = 1.0f;

        [SerializeField]
        private Color _color = Color.white;

        [SerializeField]
        private Color _borderColor = Color.white;

        [SerializeField]
        private Color _hoverColor = Color.white;

        [SerializeField]
        private float _radiusTopLeft;

        [SerializeField]
        private float _radiusTopRight;

        [SerializeField]
        private float _radiusBottomLeft;

        [SerializeField]
        private float _radiusBottomRight;

        [SerializeField]
        private float _borderInnerRadius;

        [SerializeField]
        private float _borderOuterRadius;

        private MaterialPropertyBlock block;

        #region Properties

        public float Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
                UpdateSize();
            }
        }

        public float Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
                UpdateSize();
            }
        }

        public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }

        public Color BorderColor
        {
            get
            {
                return _borderColor;
            }
            set
            {
                _borderColor = value;
            }
        }

        public Color HoverColor
        {
            get
            {
                return _hoverColor;
            }
            set
            {
                _hoverColor = value;
            }
        }

        public float RadiusTopLeft
        {
            get
            {
                return _radiusTopLeft;
            }
            set
            {
                _radiusTopLeft = value;
            }
        }

        public float RadiusTopRight
        {
            get
            {
                return _radiusTopRight;
            }
            set
            {
                _radiusTopRight = value;
            }
        }

        public float RadiusBottomLeft
        {
            get
            {
                return _radiusBottomLeft;
            }
            set
            {
                _radiusBottomLeft = value;
            }
        }

        public float RadiusBottomRight
        {
            get
            {
                return _radiusBottomRight;
            }
            set
            {
                _radiusBottomRight = value;
            }
        }

        public float BorderInnerRadius
        {
            get
            {
                return _borderInnerRadius;
            }
            set
            {
                _borderInnerRadius = value;
            }
        }

        public float BorderOuterRadius
        {
            get
            {
                return _borderOuterRadius;
            }
            set
            {
                _borderOuterRadius = value;
                UpdateSize();
            }
        }

        #endregion

        private readonly int _colorShaderID = Shader.PropertyToID("_Color");
        private readonly int _borderColorShaderID = Shader.PropertyToID("_BorderColor");
        private readonly int _hoverColorShaderID = Shader.PropertyToID("_HoverColor");
        private readonly int _radiiShaderID = Shader.PropertyToID("_Radii");
        private readonly int _dimensionsShaderID = Shader.PropertyToID("_Dimensions");
        private readonly int _hoverPositionShaderID = Shader.PropertyToID("_HoverPosition");

        protected virtual void Awake()
        {
            UpdateSize();
            UpdateMaterialPropertyBlock();
        }

        protected virtual void Start()
        {
            block = _editor.MaterialPropertyBlock;
            this.AssertField(_editor, nameof(_editor));
            UpdateSize();
            UpdateMaterialPropertyBlock();
        }

        private void UpdateSize()
        {
            transform.localScale = new Vector3(_width + _borderOuterRadius * 2,
                                               _height + _borderOuterRadius * 2,
                                               1.0f);
            UpdateMaterialPropertyBlock();
        }

        private void UpdateMaterialPropertyBlock()
        {
            if (_editor == null)
            {
                _editor = GetComponent<MaterialPropertyBlockEditor>();
                if (_editor == null)
                {
                    return;
                }
            }
            block = _editor.MaterialPropertyBlock;

            block.SetColor(_colorShaderID, _color);
            block.SetColor(_borderColorShaderID, _borderColor);
            block.SetColor(_hoverColorShaderID, _hoverColor);
            block.SetVector(_radiiShaderID,
            new Vector4(_radiusTopRight,
                        _radiusBottomRight,
                        _radiusTopLeft,
                        _radiusBottomLeft));

            block.SetVector(_dimensionsShaderID,
                new Vector4(transform.localScale.x,
                            transform.localScale.y,
                            _borderInnerRadius,
                            _borderOuterRadius));

            _editor.UpdateMaterialPropertyBlock();
        }

        private void Update()
        {
            block.SetVector(_hoverPositionShaderID,
                new Vector4(_fingerPosition.Position.x,
                            _fingerPosition.Position.y,
                            _fingerPosition.Position.z,
                            0));
        }

        protected virtual void OnValidate()
        {
            UpdateSize();
            UpdateMaterialPropertyBlock();
        }
    }
}
