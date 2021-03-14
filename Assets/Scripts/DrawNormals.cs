using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DrawNormals : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField]
    private MeshFilter _meshFilter = null;
    [SerializeField]
    private NormalsDrawData _vertexNormals = new NormalsDrawData(new Color32(200, 255, 195, 127), false);

    [System.Serializable]
    private class NormalsDrawData
    {
        [SerializeField]
        protected DrawType _draw = DrawType.Selected;
        protected enum DrawType { Never, Selected, Always }
        [SerializeField]
        protected float _length = 0.3f;
        [SerializeField]
        protected Color _normalColor;
        private Color _baseColor = new Color32(255, 133, 0, 255);
        private const float _baseSize = 0.0125f;


        public NormalsDrawData(Color normalColor, bool draw)
        {
            _normalColor = normalColor;
            _draw = draw ? DrawType.Selected : DrawType.Never;
        }

        public bool CanDraw(bool isSelected)
        {
            return (_draw == DrawType.Always) || (_draw == DrawType.Selected && isSelected);
        }

        public void Draw(Vector3 from, Vector3 direction)
        {
            if (Camera.current.transform.InverseTransformDirection(direction).z < 0f)
            {
                Gizmos.color = _baseColor;
                Gizmos.DrawWireSphere(from, _baseSize);

                Gizmos.color = _normalColor;
                Gizmos.DrawRay(from, direction * _length);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        OnDrawNormals(true);
    }

    void OnDrawGizmos()
    {
        if (!Selection.Contains(this))
            OnDrawNormals(false);
    }

    private void OnDrawNormals(bool isSelected)
    {

        Mesh mesh = _meshFilter.sharedMesh;

        //Draw Vertex Normals
        if (_vertexNormals.CanDraw(isSelected))
        {
            if (GetComponent<MeshFilter>().sharedMesh == null)
                return;

            Vector3[] vertices = mesh.vertices;
            Vector3[] normals = mesh.normals;
            for (int i = 0; i < vertices.Length; i++)
            {
                _vertexNormals.Draw(transform.TransformPoint(vertices[i]), transform.TransformVector(normals[i]));
            }
        }
    }
#endif
}