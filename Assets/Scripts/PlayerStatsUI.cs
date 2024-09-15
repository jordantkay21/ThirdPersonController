using UnityEngine;
using TMPro;

namespace KayosStudios.ThirdPersonController
{
    public class PlayerStatsUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI isGroundedText;
        [SerializeField] TextMeshProUGUI isIdleText;
        [SerializeField] TextMeshProUGUI isJumpingText;
        [SerializeField] TextMeshProUGUI moveSpeedText;
        [SerializeField] TextMeshProUGUI inputXText;
        [SerializeField] TextMeshProUGUI inputZText;
        [SerializeField] TextMeshProUGUI inclineAngleText;
        [SerializeField] TextMeshProUGUI fallDurationText;
        [SerializeField] TextMeshProUGUI currentGaitText;

        //public void UpdateUIVariables(PlayerModel model)
        //{
        //    isGroundedText.text = $"Is Grounded: {model.isGrounded}";
        //    isIdleText.text = $"Is Idle: {model.isIdle}";
        //    isJumpingText.text = $"Is Jumping: {model.isJumping}";

        //    moveSpeedText.text = $"Move Speed: {model.moveSpeed}";
        //    inputXText.text = $"Input X: {model.inputX}";
        //    inputZText.text = $"Input Z: {model.inputZ}";
        //    inclineAngleText.text = $"Incline Angle: {model.inclineAngle}";
        //    fallDurationText.text = $"Fall Duration: {model.fallDuration}";

        //    switch (model.currentGait)
        //    {
        //        case 0:
        //            currentGaitText.text = $"Current Gait: {model.currentGait} [IDLE]";
        //            break;
        //        case 1:
        //            currentGaitText.text = $"Current Gait: {model.currentGait} [WALK]";
        //            break;
        //        case 2:
        //            currentGaitText.text = $"Current Gait: {model.currentGait} [RUN]";
        //            break;
        //        case 3:
        //            currentGaitText.text = $"Current Gait: {model.currentGait} [SPRINT]";
        //            break;
        //    }

        //}
    }
}