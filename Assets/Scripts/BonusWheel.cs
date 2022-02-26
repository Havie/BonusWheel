using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Prizes;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class BonusWheel : MonoBehaviour
{

    [Header("Components")]
    [SerializeField] private Button _spinButton;
    [SerializeField] private Transform _wheelTransform;
    [SerializeField] private WheelSector[] _prizeSectors;
    [Header("Settings")] [Range(0.5f, 5f)]
    [SerializeField]private float _spinTime = 2.5f;
    [SerializeField]private int _numRotations = 4;
    [SerializeField] private AnimationCurve _spinCurve;
    
    private float _singleRotationAngle = 22.5f;
    private const int NUM_PRIZES = 8;
    private SectorDropTable _dropTable;
    private bool _isSpinning;

    /*********************************************************************/
    
    public void Start()
    {
        _singleRotationAngle = 360f / NUM_PRIZES;
        SetPrizeDisplay();
        _dropTable = new SectorDropTable(_prizeSectors);
        _spinButton.onClick.AddListener(SpinWheel);
    }

    /*********************************************************************/
    private void SpinWheel()
    {
        if (_isSpinning)
            return;
        
        var prize = _dropTable.GeneratePrize(out var sectorIndex);
        StartCoroutine(WheelRotation(sectorIndex, prize));
    }
    
    
    IEnumerator WheelRotation(int indexOfPrize, Prize prize)
    {
        _isSpinning = true;
        _spinButton.interactable = false;
        float startingAngle = _wheelTransform.eulerAngles.z;
        float endingAngle = (_numRotations * 360f) + _singleRotationAngle * indexOfPrize - startingAngle;
        //Debug.Log($"Chose Index={indexOfPrize} and endingAngle={endingAngle}");
        float currTime = 0;

        while (currTime < _spinTime)
        {
            currTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            float spinningAngle = endingAngle * _spinCurve.Evaluate(currTime / _spinTime);
            _wheelTransform.localEulerAngles = new Vector3(0, 0 ,spinningAngle + startingAngle);
        }

        //_wheelTransform.eulerAngles = new Vector3(0, 0 ,endingAngle + startingAngle);
        
        PrizePayloadHandler.IssuePrize(prize);
        _spinButton.interactable = true;
        _isSpinning = false;
    }

    private void SetPrizeDisplay()
    {
        if (_prizeSectors.Length != NUM_PRIZES)
        {
            Debug.Log($"<color=yellow>Inaccurate amount of prizes to wheel slots</color>");
        }
        for (int i = 0; i < _prizeSectors.Length; i++)
        {
            _prizeSectors[i].transform.eulerAngles = new Vector3(0, 0, -360f / NUM_PRIZES * i);
        }
    }


    private void RunUnitTests(int spinCount)
    {
        UnitTesting.OutputSectorData(_prizeSectors);

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        for (int i = 0; i < spinCount; i++)
        {
            var prize = _dropTable.GeneratePrize(out _);
            UnitTesting.AddPrize(prize);
        }
        stopwatch.Stop();
        UnitTesting.OutputPrizeResults(stopwatch.ElapsedMilliseconds);
    }

    
    
}
