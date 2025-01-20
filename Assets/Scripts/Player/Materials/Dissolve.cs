using System.Collections;
using UnityEngine;
using UnityEngine.U2D;


public class Dissolve : MonoBehaviour
{

    [SerializeField] private Material _normalMat;
    private Material normalMat;
    [SerializeField] private Material _dissolveMat;
    private Material dissolveMat;
    [SerializeField] private float _dissolveTime = 0.75f;

    [SerializeField] private GameObject _smoke;

    [SerializeField] private Transform _smokePos;

    private SpriteRenderer _spriteRenderers;
    //[SerializeField] private Material _materials;

    private int _dissolveAmount = Shader.PropertyToID("_DissolveAmount");
    private int _verticalDissolveAmount = Shader.PropertyToID("_VerticalDissolve");

    private void Awake()
    {
        this._spriteRenderers = GetComponent<SpriteRenderer>();
        this.normalMat = new Material(_normalMat);
        this.dissolveMat = new Material(_dissolveMat);
    }
    private void Start()
    {
        changeToNormalMaterial();
    }
    private IEnumerator vanish(bool useDissolve, bool useVertical)
    {
        float elapsedTime = 0f;
        while(elapsedTime < _dissolveTime)
        {
            elapsedTime += Time.deltaTime;
            float lerpedDissolve = Mathf.Lerp(0f, 1.1f, (elapsedTime / _dissolveTime));
            float lerpedVerticalDissolve = Mathf.Lerp(0f, 1.1f, (elapsedTime / _dissolveTime));

            if(useDissolve)
            _spriteRenderers.material.SetFloat(this._dissolveAmount, lerpedDissolve);
            if(useVertical)
            _spriteRenderers.material.SetFloat(this._verticalDissolveAmount, lerpedVerticalDissolve);
            Debug.Log("vanish");
            yield return null;
        }
    }private IEnumerator Appear(bool useDissolve, bool useVertical)
    {
        
        float elapsedTime = 0f;
        while(elapsedTime < _dissolveTime)
        {
            elapsedTime += Time.deltaTime;
            float lerpedDissolve = Mathf.Lerp(1.1f, 0f, (elapsedTime / _dissolveTime));
            float lerpedVerticalDissolve = Mathf.Lerp(1.1f, 0f, (elapsedTime / _dissolveTime));

            if(useDissolve)
            _spriteRenderers.material.SetFloat(this._dissolveAmount, lerpedDissolve);
            if(useVertical)
            _spriteRenderers.material.SetFloat(this._verticalDissolveAmount, lerpedVerticalDissolve);
            Debug.Log("appear");

            yield return null;
        }
    }
    public void startVanish()
    {
        changeToDissolveMaterial();
        createSmokeFX();
        StartCoroutine(vanish(true, false));
    }
    public void startAppear()
    {
        StartCoroutine(Appear(true, false));
    }
    private void changeToDissolveMaterial()
    {
        this._spriteRenderers.material = dissolveMat;
    }
    private void changeToNormalMaterial()
    {
        this._spriteRenderers.material = normalMat;
    }
    private void createSmokeFX()
    {
        var smoke = GameObject.Instantiate(_smoke, _smokePos.transform.position, Quaternion.identity);
    }
}
