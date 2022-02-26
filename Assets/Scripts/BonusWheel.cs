using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusWheel : MonoBehaviour
{

    [Header("Components")]
    [SerializeField] private Button _spinButton;
    [SerializeField] private Transform _wheelTransform;
    [SerializeField] private GameObject[] _prizes;
    [Header("Settings")] [Range(0.5f, 5f)]
    [SerializeField]private float _spinTime = 2.5f;
    [SerializeField]private int _numRotations = 4;
    [SerializeField] private AnimationCurve _spinCurve;
    
    private float _singleRotationAngle = 22.5f;
    private const int NUM_PRIZES = 8;
    
    private bool _isSpinning;

    /*********************************************************************/
    
    public void Start()
    {
        _singleRotationAngle = 360f / NUM_PRIZES;
        SetPrizeDisplay();
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
        return UnityEngine.Random.Range(1, 8);
    }
    
    IEnumerator WheelRotation(int indexOfPrize)
    {
        _isSpinning = true;
        _spinButton.interactable = false;
        float startingAngle = _wheelTransform.eulerAngles.z;
        float endingAngle = (_numRotations * 360f) + _singleRotationAngle * indexOfPrize - startingAngle;
        Debug.Log($"Chose Index={indexOfPrize} and endingAngle={endingAngle}");
        float currTime = 0;

        while (currTime < _spinTime)
        {
            currTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            float spinningAngle = endingAngle * _spinCurve.Evaluate(currTime / _spinTime);
            _wheelTransform.localEulerAngles = new Vector3(0, 0 ,spinningAngle + startingAngle);
        }

        //_wheelTransform.eulerAngles = new Vector3(0, 0 ,endingAngle + startingAngle);


        _spinButton.interactable = true;
        _isSpinning = false;
    }

    private void SetPrizeDisplay()
    {
        for (int i = 0; i < _prizes.Length; i++)
        {
            _prizes[i].transform.eulerAngles = new Vector3(0, 0, -360f / NUM_PRIZES * i);
        }
    }
    
    
}
