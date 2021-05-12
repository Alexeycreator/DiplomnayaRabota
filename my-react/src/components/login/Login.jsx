import React, { Component } from "react";
import { Table } from "react-bootstrap";

export class Login extends Component {
  constructor(props) {
    super(props);
    this.state = { logn: [] };
  }

  //Переменная для данных хранения таблицы логинов
  refreshList() {
    fetch(process.env.REACT_APP_API + "login")
      .then((response) => response.json())
      .then((data) => {
        this.setState({ logn: data });
      });
  }

  componentDidMount() {
    this.refreshList();
  }

  componentDidUpdate() {
    this.refreshList();
  }

  render() {
    const { logn } = this.state;
    return (
      <div>
        <Table className="mt-4" striped bordered hover size="sm">
          <thead>
            <tr>Id</tr>
            <tr>Login</tr>
            <tr>Password</tr>
            <tr>Midname</tr>
            <tr>Lastname</tr>
            <tr>Calendar_id</tr>
          </thead>
          <tbody>
            {logn.map(log=>
              <tr key={log.id}>
                <td>{log.Login}</td>
                <td>{log.Password}</td>
                <td>{log.Midname}</td>
                <td>{log.Lastname}</td>
                <td>{log.Calendar_id}</td>
                <td>Изменить/Удалить</td>
              </tr>
            ) }
          </tbody>
        </Table>
      </div>
    );
  }
}
