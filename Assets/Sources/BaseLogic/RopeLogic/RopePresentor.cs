using System;
using System.Collections.Generic;
using Sources.Infrastructure.RopeModel;
using UnityEngine;

namespace Sources.BaseLogic.RopeLogic
{
    public class RopePresentor : IDisposable
    {
        private readonly RopeModel _model;
        private readonly Rope _view;

        public RopePresentor(RopeModel model, Rope view)
        {
            _model = model;
            _view = view;
            
            _view.SetUp(_model.StartPoint, _model.EndPoint);

            _model.Moved += OnMoved;
            _model.Updated += OnUpdated;
        }
        
        public void Dispose()
        {
            _model.Moved -= OnMoved;
            _model.Updated -= OnUpdated;
        }
        
        private void OnUpdated(IReadOnlyList<Vector3> points) =>
            _view.UpdateRenderer(points);

        private void OnMoved(int endPointIndex, Vector3 position) =>
            _view.UpdateEndPoint(endPointIndex, position);
    }
}