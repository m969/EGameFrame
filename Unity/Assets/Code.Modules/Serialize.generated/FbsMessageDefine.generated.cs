using ET;

namespace EGameFrame.Message
{
    [Message(1)]
    public partial class FooBarContainer : IMessage
    {
    }

    [Message(2)]
    public partial class LoginRequest : IRequest
    {
    }

    [Message(3)]
    public partial class LoginResponse : IResponse
    {
    }

    [Message(4)]
    public partial class Monster : IMessage
    {
    }


}
