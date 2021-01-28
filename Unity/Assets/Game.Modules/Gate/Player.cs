using System.Collections;
using System.Collections.Generic;

namespace EGameFrame.Gate
{
    public partial class Player : Entity
    {
        public AccountActor AccountEntity { get; set; }
        public long UnitId { get; set; }
    }
}