using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlecableObject : MonoBehaviour
{
    public bool Placed { get; private set; }
    public Vector3Int Size { get; private set; }
    private Vector3[] Verticres;
    [SerializeField] ParticleSystem part;

    public List<EnumsAndStructs.Resource> nearResources;

    public EnumsAndStructs.Resource[] neededResources;

    private void Start()
    {
        GetColliderVertexPositiosLocal();
        CalculateSizeInCells();
    }

    public virtual bool Place()
    {
        if(!ResourcesMenager.instance.usedResources(neededResources))
            return false;

        ObjectDrag drag = gameObject.GetComponent<ObjectDrag>();
        Destroy(drag);
        Instantiate(part, transform.position - new Vector3(0,0,0), Quaternion.identity);

        Placed = true;
        return true;
    }

    private void GetColliderVertexPositiosLocal()
    {
        BoxCollider b = gameObject.GetComponent<BoxCollider>();
        Verticres = new Vector3[4];
        Verticres[0] = b.center + new Vector3(-b.size.x, -b.size.y, -b.size.z) * 0.5f;
        Verticres[1] = b.center + new Vector3(b.size.x, -b.size.y, -b.size.z) * 0.5f;
        Verticres[2] = b.center + new Vector3(b.size.x, -b.size.y, b.size.z) * 0.5f;
        Verticres[3] = b.center + new Vector3(-b.size.x, -b.size.y, b.size.z) * 0.5f;
    }
    private void CalculateSizeInCells()
    {
        Vector3Int[] verticles = new Vector3Int[Verticres.Length];

        for (int i = 0; i < verticles.Length; i++)
        {
            Vector3 wordPos = transform.TransformPoint(Verticres[i]);
            verticles[i] = BuildingSystem.instance.gridLayout.WorldToCell(wordPos);
        }

        Size = new Vector3Int(
            Mathf.Abs((verticles[0] - verticles[1]).x),
            Mathf.Abs((verticles[0] - verticles[3]).y),
            1
            );
    }
    public Vector3 GetStartPosition()
    {
        return transform.TransformPoint(Verticres[0]);
    }

    public Vector3 GetStartPositionWithOffset(Vector3 offset)
    {
        return transform.TransformPoint(Verticres[0] + offset);
    }

}
