using ET;

namespace EGameFrame.Message
{
    [Message(101)]
    public partial class FooBarContainer : IMessage
    {
    }

    [Message(102)]
    public partial class LoginRequest : IRequest
    {
    }

    [Message(103)]
    public partial class LoginResponse : IResponse
    {
    }

    [Message(104)]
    public partial class Monster : IMessage
    {
    }


}
