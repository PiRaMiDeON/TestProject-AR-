using Ricimi;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ModelsController : MonoBehaviour
{
    private GameObject _currentModel;
    private Color _currentColor = Color.white;
    private Animator _currentAnimator;
    private List<GameObject> _currentAnimationsButtons;

    [SerializeField] private RectTransform _animationsScrollViewContent;
    [SerializeField] private GameObject _buttonPrefab;

    [SerializeField] private ARRaycastManager _raycastManager;
    [SerializeField] private Camera _arCamera;

    private List<ARRaycastHit> _hits = new List<ARRaycastHit>();

    public void SpawnModel(GameObject model)
    {
        GameObject modelOnTheScene = GameObject.FindGameObjectWithTag("Model");

        if (modelOnTheScene != null)
        {
            Destroy(modelOnTheScene);
        }

        ChangeModel(model);
    }

    public void ChangeModelAnimation()
    {
        if(_currentAnimationsButtons == null)
        {
            _currentAnimationsButtons = new List<GameObject>();
        }

        if (_currentAnimationsButtons.Count != 0)
        {
            for (int i = 0; i < _currentAnimationsButtons.Count; i++)
            {
                Destroy(_currentAnimationsButtons[i]);
            }
            _currentAnimationsButtons.Clear();
        }

        if (_currentAnimator != null)
        {

            RuntimeAnimatorController animator = _currentAnimator.runtimeAnimatorController;

            AnimationClip[] clips = animator.animationClips;
            Debug.Log(clips.Length);

            for (int i = 0; i < clips.Length; i++)
            {
                GameObject spawnedButton = Instantiate(_buttonPrefab, _animationsScrollViewContent);
                spawnedButton.GetComponentInChildren<TMP_Text>().text = clips[i].name;

                int clipIndex = i;

                spawnedButton.GetComponent<Button>().onClick.AddListener(() => AnimateModel(clips[clipIndex].name));
                _currentAnimationsButtons.Add(spawnedButton);
            }
        }
    }

    private void AnimateModel(string animationName)
    {
        _currentAnimator.SetTrigger(animationName);
    }

    public void ChangeModel(GameObject newModel)
    {
        Ray ray = new Ray(_arCamera.transform.position, _arCamera.transform.forward);

        // Выполняем AR-каст по плоскости в направлении луча
        if (_raycastManager.Raycast(ray, _hits, TrackableType.PlaneWithinPolygon))
        {
            // Берем первую пересеченную плоскость
            Pose hitPose = _hits[0].pose;

            // Спавним модель на поверхности
            _currentModel = Instantiate(newModel, hitPose.position, hitPose.rotation);

            // Убедимся, что модель выровнена относительно плоскости
            _currentModel.transform.up = Vector3.up; // Гарантируем, что объект ровно стоит на плоскости
            _currentAnimator = _currentModel.GetComponent<Animator>();

            ChangeModelColor(_currentColor);
            ChangeModelAnimation();
        }
        else
        {
            Debug.Log("Плоскость не найдена. Убедитесь, что камера смотрит на плоскость.");
        }
    }

    public void ChangeModelColor(Color color)
    {
        if (_currentModel != null)
        {
            _currentColor = color;

            Renderer[] childrenGameobjects = _currentModel.GetComponentsInChildren<Renderer>();

            for (int i = 0; i < childrenGameobjects.Length; i++)
            {
                if (childrenGameobjects[i].tag == "MaterialGameobject")
                {
                    childrenGameobjects[i].material.color = _currentColor;
                    break;
                }
            }
        }
    }

    public void ClearColor()
    {
        ChangeModelColor(Color.white);
    }
    public void SetColorRed()
    {
        ChangeModelColor(Color.red);
    }

    public void SetColorGreen()
    {
        ChangeModelColor(Color.green);
    }

    public void SetColorBlue()
    {
        ChangeModelColor(Color.blue);
    }

    public void SetColorBlack()
    {
        ChangeModelColor(Color.black);
    }

    public void SetColorYellow()
    {
        ChangeModelColor(Color.yellow);
    }

    public void SetColorCyan()
    {
        ChangeModelColor(Color.cyan);
    }

    public void SetColorPink()
    {
        ChangeModelColor(Color.magenta);
    }
}
