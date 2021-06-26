class User extends React.Component {

    constructor(props) {
        super(props);
        this.state = { data: props.user };
    }
    render() {
        return <div>
            <p><b>{this.state.data.id}</b></p>
            <p>Дата {this.state.data.registrationDate}</p>
            <p>Дата {this.state.data.lastActivityDate}</p>
        </div>;
    }
}

class UsersList extends React.Component {

    constructor(props) {
        super(props);
        this.state = { users: [] };
    }
    // загрузка данных
    loadData() {
        var xhr = new XMLHttpRequest();
        xhr.open("get", this.props.apiUrl, true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            this.setState({ users: data });
        }.bind(this);
        xhr.send();
    }
    componentDidMount() {
        this.loadData();
    }
    render() {

        return <div>
            <h2>Список пользователей</h2>
            <div>
                {
                    this.state.users.map(function (user) {

                        return <User key={user.id} user={user} />
                    })
                }
            </div>
        </div>;
    }
}

ReactDOM.render(
    <UsersList apiUrl="/api/users" />,
    document.getElementById("content")
);