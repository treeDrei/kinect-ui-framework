﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectControll.Demo.View.BaseView
{
    public interface IKinectView
    {
        void SetState(String state);
        void SetParent(IKinectView parent);
        IKinectView GetParent();
    }   
}
