namespace DefaultNamespace
{
    public class Notes
    {
        
    }
    
    
    //TODO change the way it fires: increase range and rate of fire of turret to test
    // change two finger touch. it acts like a mouse click and averages the two into a center click. can also be used as a game idea
    //change missile turn speed
    // game idea at 2:08 LORN - ANVIL [Official Music Video] for lightspeed game. When they appear, add particles or lights temporarily
    
    
    //TODO fix rotation: if there is a rotation, that means the 2 touch should be used. if no rotation, then 1 touch.
	//https://docs.unity3d.com/Manual/MobileInput.html
	//^ delta time the time it takes for one finger to be lifted to the next. go from 2 fingers to 1 tecnhnically for a second which is fine
	// , it should work the same BUT if the last finger is lifted, then the 2 finger rotation is set and the 1 finger rotation is gone. completely eliminated until the first finger is
	//set again which would cancel out any rotations anyway. Also take the average over the last .1 seconds . get an average or like an array of the last 4 updates or even last 2-3
	//rider with many levels (glow lines and the longer that goes on the less there are or the more squeezed they get. can accelerated by tapping. perfect for new 2018.3 particle system to make it look good
	//Update turret position based on if the node is moving? like an elevator.
	
	//var distance = heading.magnitude;
	//var direction = heading / distance; // This is now the normalized direction.
	//https://docs.unity3d.com/Manual/DirectionDistanceFromOneObjectToAnother.html
	
	
	
	//FIX BEFORE RELEASE
	//TODO change the enemy impact effect from the bullet to the enemy. the enemy should decide that bc if there is another enemy then it needs to have a different particle system spawned
	//TODO should not be under the bullet prefab particle effect
	//change the shop update to update a current cost to reflect the upgraded turrets cost
	//TODO change the green squares to only when something is built, not when its moused over.
	
	
	
	
	/*
	 
	 
	 
	 
	 
	 
	 
	 //TODO GAME IDEAS----------------------
	 //create an AOE that when applied to enemies, the next attacks are strong but the are faster or something
	 //another aoe which pulls them to the center making it more effecient for other aoes. stuns/and or slows them at first
	 //ENEMY AND BULLET POOLING. PARTICLE SYSTEMS
	 //insane laser beam what chargers up when pressed? just a large laser beam. same prefab. takes out anything in its path
	 
	 //slicing finger across the screen creates a trap or something thats slows or hurts them.
	 //ability tree
	 //some sort of visual effect for the explosion. explore the particle pack? circle maybe
	 
	 
	 
	 
	 */
	
	
	
	
}




