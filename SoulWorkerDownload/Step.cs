using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoulWorkerDownload
{
    public static class Step
    {
        public enum State
        {
            _ProcessState_begin = 0,
            _ProcessState_Loading,
            _ProcessState_Pause,
            _ProcessState_Restart,

            _ProcessState_LoadEnd,
            //_ProcessState_Unzip,
            //_ProcessState_UnzipEnd,
            _ProcessState_Ready,

            _ProcessState_Wait,
            _ProcessState_Exit,
        }
        public static State CURRENT_STEP = State._ProcessState_begin;

    }
}
