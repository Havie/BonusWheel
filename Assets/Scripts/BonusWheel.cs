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
        GenerateDropTable();
        _spinButton.onClick.AddListener(SpinWheel);
    }

    private void GenerateDropTable()
    {
        _dropTable = new SectorDropTable(_prizeSectors);
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

    /*************************************************************************************************************************************/
    //TESTING:   can be run from three dots on component (would write a custom inspector to expose button for designer in real project)
    /*************************************************************************************************************************************/
    [ContextMenu("RunDesignerSectorTest")]
    private void RunDesignerSectorTest()
    {
        UnitTesting.OutputSectorData(_prizeSectors);
    }

    [ContextMenu("Run1000UnitTests")]
    private void Run1000UnitTests()
    {
        if (_dropTable == null)
            GenerateDropTable();
        
        const int ITERATIONS = 1000;
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        for (int i = 0; i < ITERATIONS; i++)
        {
            var prize = _dropTable.GeneratePrize(out _);
            UnitTesting.KeepTrackOfPrize(prize);
        }
        stopwatch.Stop();
        UnitTesting.OutputPrizeResults(stopwatch.ElapsedMilliseconds , ITERATIONS);
    }

    
    
}
