using System.Collections.Generic;
using CodeBase.Buildings;
using UnityEngine;

namespace CodeBase.Actors
{
    public class ActorFactory : MonoBehaviour
    {
        public List<Actor> SceneActors;
        
        public IReadOnlyCollection<Actor> Actors => SceneActors;

        public Actor RobotTemplate;
        public MainCapsule MainCapsule;
        
        public void CreateRobot(Transform at)
        {
            var robot = Instantiate(RobotTemplate, at.position, Quaternion.identity);
            SceneActors.Add(robot);
            robot.Constructor(MainCapsule);
        }
    }
}