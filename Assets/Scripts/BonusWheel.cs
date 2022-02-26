using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusWheel : MonoBehaviour
{

    [Header("Components")]
    [SerializeField] private Button _spinButton;
    [SerializeField] private Transform _wheelTransform;
    [Header("Settings")] [Range(0.5f, 5f)]
    [SerializeField]private float _spinTime = 2.5f;
    [SerializeField] private AnimationCurve _spinCurve;
    
    private const float SINGLE_ROTATION_ANGLE = 22.5f;
    private const int NUM_PRIZES = 8;
    
    private bool _isSpinning;

    /*********************************************************************/
    
    public void Start()
    {
        _spinButton.onClick.AddListener(SpinWheel);
    }

    /*********************************************************************/
    private void SpinWheel()
    {
        if (_isSpinning)
            return;
        
        StartCoroutine(WheelRotation(GetIndexOfRandomPrize()));
    }

    private int GetIndexOfRandomPrize()
    {
        //TODO- base off dropchances
        return UnityEngine.Random.Range(0, 8);
    }
    
    IEnumerator WheelRotation(int indexOfPrize)
    {
        _isSpinning = true;
        _spinButton.interactable = false;
        float endingAngle = (NUM_PRIZES * 360) + SINGLE_ROTATION_ANGLE + indexOfPrize;
        //float startingAngle = _wheelTransform.eulerAngles.z;
        Debug.Log($"Chose Index={indexOfPrize} and endingAngle={endingAngle}");
        float currTime = 0;

        while (currTime < _spinTime)
        {
            currTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            float spinningAngle = endingAngle * _spinCurve.Evaluate(currTime / _spinTime);
            _wheelTransform.eulerAngles = new Vector3(0, 0 ,spinningAngle);
        }

        _wheelTransform.eulerAngles = new Vector3(0, 0 ,endingAngle);


        _spinButton.interactable = true;
        _isSpinning = false;
    }
    
    
}
