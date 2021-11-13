using Godot;

public class AnimationController {
    public AnimationPlayer animationPlayer;
	public AnimationTree animationTree;
	public AnimationNodeStateMachinePlayback animationState;

    public AnimationController(Player player) {
        animationPlayer = player.GetNode<AnimationPlayer>("AnimationPlayer");
		animationTree = player.GetNode<AnimationTree>("AnimationTree");
		animationState = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");
    }

    public void Update() {
    }
    public void Travel(string toNode) {
        animationState.Travel(toNode);
    }
}
