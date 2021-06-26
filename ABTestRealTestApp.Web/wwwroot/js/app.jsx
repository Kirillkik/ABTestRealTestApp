class User extends React.Component {

    constructor(props) {
        super(props);
        this.state = { data: props.user };
       
    }

    handleRegistrationDateChanged(event) {
        var item = this.state.data;

        item.registrationDate = event.target.value;

        this.setState({
            data: item
        });
    }

    handleLastActivityDateChanged(event) {
        var item = this.state.data;

        item.lastActivityDate = event.target.value;

        this.setState({
            data: item
        });
    }

    render() {
        return <tr>
            <td>{this.state.data.id}</td>
            <td><input type="text" value={this.state.data.registrationDate} onChange={this.handleRegistrationDateChanged.bind(this)} /></td>
            <td><input type="text" value={this.state.data.lastActivityDate} onChange={this.handleLastActivityDateChanged.bind(this)} /></td>
        </tr>;
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
            <table border = "1">
                {
                    this.state.users.map(function (user) {

                        return <User key={user.id} user={user} />
                    })
                }
            </table>
        </div>;
    }
}

ReactDOM.render(
    <UsersList apiUrl="/api/users" />,
    document.getElementById("content")
);