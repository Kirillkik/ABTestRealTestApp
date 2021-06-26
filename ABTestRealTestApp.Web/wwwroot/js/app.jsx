class User extends React.Component {

    constructor(props) {
        super(props);
        this.state = { data: props.user };
        this.onClick = this.onClick.bind(this);
    }

    onClick(e) {
        this.props.onRemove(this.state.data);
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
            <td><input type="button" value="Удалить" onClick={this.onClick} /></td>
        </tr>;
    }
}

class UsersList extends React.Component {

    constructor(props) {
        super(props);
        this.state = { users: [] };
        this.onSubmit = this.onSubmit.bind(this);
        this.onRemoveUser = this.onRemoveUser.bind(this);
        this.onAddUser = this.onAddUser.bind(this);
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

    onRemoveUser(user) {
        if (user) {
            var url = this.props.apiUrl + "/" + user.id;

            var xhr = new XMLHttpRequest();
            xhr.open("delete", url, true);
            xhr.setRequestHeader("Content-Type", "application/json");
            xhr.onload = function () {
                if (xhr.status === 200) {
                    this.loadData();
                }
            }.bind(this);
            xhr.send();
        }
    }

    onAddUser(user) {
        var xhr = new XMLHttpRequest();
        xhr.open("post", this.props.apiUrl, true);
        xhr.onload = function () {
            if (xhr.status === 200) {
                this.loadData();
            }
        }.bind(this);
        xhr.send();
    }

    onSubmit(e) {
        e.preventDefault();
        var data = new FormData();
        data.append("Users", JSON.stringify(this.state.users));

        var url = this.props.apiUrl + "/update";

        fetch(url,
            {
                method: "POST",
                body: data
            }).
            then(response => response.text())
            .then(data => {
                this.setState({ text: data, loading: false });
            });
    }

    render() {
        var remove = this.onRemoveUser;
        return <div>
            <h2>Список пользователей</h2>
            <form onSubmit={this.onSubmit}>
                <table>
                    <tr>
                        <th>Id</th>
                        <th>Registaration Date</th>
                        <th>Last Activity Date</th>
                    </tr>
                    {
                        this.state.users.map(function (user) {
                            return <User key={user.id} user={user} onRemove={remove} />
                        })
                    }
                </table>
                <br/>
                <input type="submit" value="Сохранить" />
            </form>
            <br />
            <input type="button" value="Добавить строку" onClick={this.onAddUser} />
        </div>;
    }
}

ReactDOM.render(
    <UsersList apiUrl="/api/users" />,
    document.getElementById("content")
);