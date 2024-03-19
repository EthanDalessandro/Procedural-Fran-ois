using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LegDataInfo
{
    [SerializeField] public Transform _rigHintTransform;
    [SerializeField] public Transform _rigTargetTransform;
    [SerializeField] public Transform _refTransform;
    [SerializeField] public Transform _baseTransform;

    [SerializeField] public float _animationTime = 0.2f;
    [SerializeField] public AnimationCurve _animationCurve;

    private bool _isAnimated = false;
    private float _animationTimeRemaining = 0;
    private Vector3 _startAnimationPosition;

    public bool IsAnimated { get { return _isAnimated; } }

    public void StartAnimation()
    {
        _isAnimated = true;
        _animationTimeRemaining = _animationTime;
        _startAnimationPosition = _rigTargetTransform.position;
    }

    public void UpdateAnimation()
    {
        if (_isAnimated)
        {
            _animationTimeRemaining -= Time.deltaTime;

            _rigTargetTransform.position = Vector3.Lerp(_startAnimationPosition, _baseTransform.position, _animationCurve.Evaluate(1 - _animationTimeRemaining / _animationTime));

            if (_animationTimeRemaining <= 0)
            {
                _isAnimated = false;
            }
        }
    }

}

public class LegsController : MonoBehaviour
{
    [SerializeField] private List<LegDataInfo> _legs;

    [SerializeField] private float _maxDistanceLegTarget = 0.2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateBaseLegs();
        UpdateRigTargets();

        foreach (LegDataInfo leg in _legs)
        {
            leg.UpdateAnimation();
        }

    }

    private void UpdateRigTargets()
    {

        int countMovingLegs = 0;
        foreach (LegDataInfo leg2 in _legs)
        {
            if (leg2.IsAnimated)
                countMovingLegs++;
        }

        if (countMovingLegs > 2)
            return;

        foreach (LegDataInfo leg in _legs)
        {
            if (Vector3.Distance(leg._baseTransform.position, leg._rigTargetTransform.position) > _maxDistanceLegTarget)
            {
                leg.StartAnimation();
                return;
            }
        }
    }

    private void UpdateBaseLegs()
    {
        foreach (LegDataInfo leg in _legs)
        {
            RaycastHit hit;

            if (Physics.Raycast(leg._refTransform.position, Vector3.down, out hit))
            {
                leg._baseTransform.position = hit.point;
            }
        }
    }
}