using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhenFullScreen
{
    public class EdgeEvent
    {
        public EdgeEvent(bool defaultSignal = false)
        {
            m_prevSignal = defaultSignal;

            WhenRisingEdge = null;
            WhenFallingEdge = null;
        }

        /*---------------------------------------------------------------------------------*/

        private bool m_prevSignal;

        /*---------------------------------------------------------------------------------*/

        public Action WhenRisingEdge
        {
            get; set;
        }

        public Action WhenFallingEdge
        {
            get; set;
        }

        /*---------------------------------------------------------------------------------*/

        public void SetSignal(bool bDigital)
        {
            if (m_prevSignal != bDigital)
            {
                if (bDigital)
                {
                    if (WhenRisingEdge != null)
                        WhenRisingEdge();
                }
                else
                {
                    if (WhenFallingEdge != null)
                        WhenFallingEdge();
                }
            }

            m_prevSignal = bDigital;
        }
    }
}
