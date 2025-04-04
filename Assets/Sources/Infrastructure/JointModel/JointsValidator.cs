using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Sources.Infrastructure.JointModel
{
    public class JointsValidator :  IEnumerable<JointModel>
    {
        private readonly List<JointModel> _joints;

        public JointsValidator(List<JointValidationData> validationDatas)
        {
            _joints = validationDatas.Select(value => new JointModel(value.Position)).ToList();
        }

        public IEnumerator<JointModel> GetEnumerator() =>
            _joints.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }
}