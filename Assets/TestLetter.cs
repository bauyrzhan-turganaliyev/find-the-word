using DG.Tweening;
using UnityEngine;

public class TestLetter : MonoBehaviour
{
    [SerializeField] private Transform _transform;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _transform.DOScale(Vector3.one, 1f);
        }
    }
}
