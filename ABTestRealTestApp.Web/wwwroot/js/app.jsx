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
        return <div class="row">
            <div class="cell">{this.state.data.id}</div>
            <div class="cell"><input type="text" value={this.state.data.registrationDate} onChange={this.handleRegistrationDateChanged.bind(this)} /></div>
            <div class="cell"><input type="text" value={this.state.data.lastActivityDate} onChange={this.handleLastActivityDateChanged.bind(this)} /></div>
            <div class="cell"><input class="tButton" type="button" value="Delete" onClick={this.onClick} /></div>
        </div>;
    }
}

class UsersList extends React.Component {

    constructor(props) {
        super(props);
        this.state = { users: [], rollingRetentionSevenDay: "", validateMessage: ""};
        this.onSubmit = this.onSubmit.bind(this);
        this.onRemoveUser = this.onRemoveUser.bind(this);
        this.onAddUser = this.onAddUser.bind(this);
        this.onCalculate = this.onCalculate.bind(this);
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
                this.setState({ text: data, loading: false, validateMessage: data });
            });
    }

    onCalculate() {
        this.loadDurationOfLifeHistogram();
        this.loadRollingRetentionSevenDay();
    }

    loadDurationOfLifeHistogram() {
        var url = this.props.apiUrl + "/durationoflifehistogram";
        var xhr = new XMLHttpRequest();
        xhr.open("get", url, true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            var chart = new CanvasJS.Chart("chartContainer", {
                theme: "light2",
                animationEnabled: true,
                title: {
                    text: "Duration of life histogram"
                },
                data: [
                    {
                        type: "column",
                        dataPoints: data,
                    }
                ]
            });
            chart.render();
        }.bind(this);
        xhr.send();
    }

    loadRollingRetentionSevenDay() {
        var url = this.props.apiUrl + "/getrollingretentionsevenday";
        var xhr = new XMLHttpRequest();
        xhr.open("get", url, true);
        xhr.onload = function () {
            var data = "Rolling Retention 7 day: " + JSON.parse(xhr.responseText) + "%";
            this.setState({ rollingRetentionSevenDay: data });
        }.bind(this);
        xhr.send();
    }

    render() {
        var remove = this.onRemoveUser;
        return <div>
            <form onSubmit={this.onSubmit}>
                <div class="container-table100">
                <div class="wrap-table100">
                    <div class="table">
                        <div class="row header">
                            <div class="cell">Id</div>
                            <div class="cell">Registaration Date</div>
                            <div class="cell">Last Activity Date</div>
                            <div class="cell"></div>
                        </div>
                        {
                            this.state.users.map(function (user) {
                                return <User key={user.id} user={user} onRemove={remove} />
                            })
                        }
                    </div>
                </div>
                </div>
                <br />
                <p class="validateText">{this.state.validateMessage}</p>
                <div class="buttonBox">
                    <input class="button" type="button" value="AddRow" onClick={this.onAddUser} />
                    <input class="button" type="submit" value="Save" />
                    <input class="button" type="button" value="Calculate" onClick={this.onCalculate} />
                </div>
            </form>
            <br />
            <p class="rollingRetentionText">{this.state.rollingRetentionSevenDay}</p>
        </div>;
    }
}

ReactDOM.render(
    <UsersList apiUrl="/api/users" />,
    document.getElementById("content")
);