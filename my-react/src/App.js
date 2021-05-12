import "./App.css";

import { BrowserRouter, Route, Switch } from "react-router-dom";

import { Home } from "./components/home/Home";
import { GoogleCalendar } from "./components/calendar/GoogleCalendar";
import { YandexCalendar } from "./components/calendar/YandexCalendar";
import { OutlookCalendar } from "./components/calendar/OutlookCalendar";
import { MairRUCalendar } from "./components/calendar/MairRUCalendar";
import { Login } from "./components/login/Login";
import { Navigation } from "./components/navigation/Navigation";
import { Sobitie } from "./components/sobitie/Sobitie";

function App() {
  return (
    <BrowserRouter>
      <div className="container">
        <h3 className="m-3 d-flex justify-content-center">
          Веб-приложение "Агрегатор календарей"
        </h3>

        <Navigation />

        <Switch>
          <Route path="/" component={Home} exact />
          <Route path="/google" component={GoogleCalendar} />
          <Route path="/yandex" component={YandexCalendar} />
          <Route path="/outlook" component={OutlookCalendar} />
          <Route path="/mailru" component={MairRUCalendar} />
          <Route path="/login" component={Login} />
          <Route path="/sobitie" component={Sobitie} />
        </Switch>
      </div>
    </BrowserRouter>
  );
}

export default App;
