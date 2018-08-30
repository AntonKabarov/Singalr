class Hello extends React.Component {
    render() {
        return <h2>Чат комната</h2>;
    }
}
ReactDOM.render(
    <Hello />,
    document.getElementById("p")
);
class UserForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = { name: "" };

        this.onChange = this.onChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }
    onChange(e) {
        var val = e.target.value;
        this.setState({ name: val });
    }

    handleSubmit(e) {
        e.preventDefault();
        alert("Логин: " + this.state.name);
    }

    render() {
        return (
            <div onSubmit={this.handleSubmit} style={{ fontFamily: 'Verdana' }}>
                <h2>Авторизация</h2>
                <p>
                    <label>Логин:</label><br />
                    <input id="txtUserName" type="text" value={this.state.name} onChange={this.onChange} />
                </p>
                <p>
                    <label>Пароль:</label><br />
                    <input id="txtPassword" type="password" />
                </p>

                <input id="btnLogin" type="button" value="Войти" />
            </div>
        );
    }
}

ReactDOM.render(
    <UserForm />,
    document.getElementById("loginBlock")
)
