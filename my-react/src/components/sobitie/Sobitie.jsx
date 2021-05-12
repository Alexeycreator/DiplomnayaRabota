import React, { Component } from "react";
import { Table } from "react-bootstrap";

export class Sobitie extends Component {
  constructor(props) {
    super(props);
    this.state = { sobs: [] };
  }

  //Переменная для данных хранения таблицы событий
  refreshList() {
    fetch(process.env.REACT_APP_API + "sobitie")
      .then((response) => response.json())
      .then((data) => {
        this.setState({ sobs: data });
      });
  }

  componentDidMount() {
    this.refreshList();
  }

  componentDidUpdate() {
    this.refreshList();
  }

  render() {
    const { sobs } = this.state;
    return (
      <div>
        <Table className="mt-4" striped bordered hover size="sm">
          <thead>
            <tr>Id</tr>
            <tr>Calendar_id</tr>
            <tr>Name</tr>
            <tr>Opisanie</tr>
            <tr>Data</tr>
            <tr>Options</tr>
          </thead>
          <tbody>
            {sobs.map((sob) => (
              <tr key={sob.id}>
                <td>{sob.id}</td>
                <td>{sob.calendar_id}</td>
                <td>{sob.name}</td>
                <td>{sob.opisanie}</td>
                <td>{sob.data}</td>
                <td>Изменить/Удалить</td>
              </tr>
            ))}
          </tbody>
        </Table>
      </div>
    );
  }
}
