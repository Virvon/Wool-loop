using System.Collections.Generic;
using Sources.Infrastructure.JointModel;

namespace Sources.BaseLogic.JointLogic
{
    public class JointsValidatorPresentor
    {
        private readonly JointsValidator _model;
        private readonly JointListView _listview;

        private List<JointPresentor> _jointsPresentors;

        public JointsValidatorPresentor(JointsValidator model, JointListView listview)
        {
            _model = model;
            _listview = listview;

            _jointsPresentors = new();

            foreach (JointModel jointModel in _model)
            {
                Joint joint = _listview.SpawnItem();
                joint.transform.position = jointModel.Position;
                JointPresentor jointPresentor = new(jointModel, joint);
                
                _jointsPresentors.Add(jointPresentor);
            }
        }
    }
}