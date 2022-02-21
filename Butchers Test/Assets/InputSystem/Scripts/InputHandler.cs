using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InputSystem
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] [Range (0, 20)]
        private int _inputDelay;
        private int _inputDuration;
        private EventSystem _eventSystem;
        private SplineDrawer _splineDrawer;
        
        private void Awake() 
        {
            _splineDrawer = FindObjectOfType<SplineDrawer>();
            _eventSystem = FindObjectOfType<EventSystem>();
        }

        void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                _splineDrawer.DrawingBegan();
                _inputDuration = 0;
                return;
            }
            
            if(Input.GetMouseButtonUp(0))
            {
                _splineDrawer.DrawingEnded();
                return;
            }
            
            if(Input.anyKey == false) return;
            
            PointerEventData eventData = new PointerEventData(_eventSystem);
            eventData.position = Input.mousePosition;
            List<RaycastResult> hits = new List<RaycastResult>();
            _eventSystem.RaycastAll(eventData, hits);
            if(hits.Count == 0) return;
            Vector3 inputPos = hits[0].worldPosition;

            if(Input.GetMouseButton(0) == false) return;
            
            if(_inputDuration < _inputDelay)
            {
                _inputDuration++;
                return;
            }
            else
            {
                _inputDuration = 0;
                _splineDrawer.DrawingContinues(inputPos);
                return;
            }
        }
    }
}