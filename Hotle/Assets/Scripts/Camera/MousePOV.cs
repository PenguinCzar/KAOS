using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CameraRig))]
public class MousePOV : MonoBehaviour
{
   public float XSensitivity = 2f;
        public float YSensitivity = 2f;
        public bool clampVerticalRotation = true;
        public float MinimumX = -90F;
        public float MaximumX = 90F;
        public bool smooth;
        public float smoothTime = 5f;
        public bool lockCursor = true;


        private Quaternion yAxis;
        private Quaternion xAxis;
        private CameraRig Crig;
        private bool m_cursorIsLocked = false;
        
        void Start(){
            Crig = GetComponent<CameraRig>();
        }
        void Update(){
            if(Input.GetMouseButton(0) && (Input.GetAxis("Mouse X") !=  0 || (Input.GetAxis("Mouse Y") !=  0))){
                if(GameManager.ins.ivCanvas.gameObject.activeInHierarchy || GameManager.ins.obsCamera.gameObject.activeInHierarchy)
                    return;
                    
                yAxis = Crig.y_axis.localRotation;
                xAxis = Crig.x_axis.localRotation;
                LookRotation();
            }
        }


        public void LookRotation()
        {
            float yRot = Input.GetAxis("Mouse X") * XSensitivity;
            float xRot = Input.GetAxis("Mouse Y") * YSensitivity;

            yAxis *= Quaternion.Euler (0f, yRot, 0f);
            xAxis *= Quaternion.Euler (-xRot, 0f, 0f);

            if(clampVerticalRotation)
                xAxis = ClampRotationAroundXAxis (xAxis);

            if(smooth)
            {
                Crig.y_axis.localRotation = Quaternion.Slerp (Crig.y_axis.localRotation, yAxis,
                    smoothTime * Time.deltaTime);
                Crig.x_axis.localRotation = Quaternion.Slerp (Crig.x_axis.localRotation, xAxis,
                    smoothTime * Time.deltaTime);
            }
            else
            {
                Crig.y_axis.localRotation = yAxis;
                Crig.x_axis.localRotation = xAxis;
            }

            UpdateCursorLock();
        }

        public void SetCursorLock(bool value)
        {
            lockCursor = value;
            if(!lockCursor)
            {//we force unlock the cursor if the user disable the cursor locking helper
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        public void UpdateCursorLock()
        {
            //if the user set "lockCursor" we check & properly lock the cursos
            if (lockCursor)
                InternalLockUpdate();
        }

        private void InternalLockUpdate()
        {
            if(Input.GetKeyUp(KeyCode.Escape))
            {
                m_cursorIsLocked = false;
            }
            else if(Input.GetMouseButtonUp(0))
            {
                m_cursorIsLocked = true;
            }

            if (m_cursorIsLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else if (!m_cursorIsLocked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

            angleX = Mathf.Clamp (angleX, MinimumX, MaximumX);

            q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }
}
