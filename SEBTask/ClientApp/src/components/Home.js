import React from 'react';
import { connect } from 'react-redux';
import '../styles/homeStyle.css';
import axios from 'axios';

class Home extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            selectedDate: null,
            exchangeRates: []
        };
    }

    selectNewDate = (newDate) => {
        this.setState({ selectedDate: newDate });
    }

    getExchangeRates = () => {
        if (this.state.selectedDate === null) {
            alert("Please select the date");
            return;
        }
        axios.get(document.location.origin + '/api/exchangerate/GetExchangeRatesChange?selectedDate=' + this.state.selectedDate)
            .then(res => {
                this.setState({ exchangeRates: res.data });
            }).catch((err) => {
                alert("Something went wrong");
                this.setState({ exchangeRates: [] });
            });
    };

    render() {
        return (
            <div className="container">
                <h1>SEB task</h1>
                <div>
                    Please select the date: <input type="date" name="dateInput" onChange={(e) => this.selectNewDate(e.target.value)} />
                </div>
                <button type="button" onClick={this.getExchangeRates}> Submit</button>
                {
                    (this.state.exchangeRates.length !== 0 ?
                        <table border="1">
                            <tr>
                                <th>Currency</th>
                                <th>Change for currency rate</th>
                                <th>Previous rate</th>
                                <th>Current rate</th>
                            </tr>
                            {
                                this.state.exchangeRates.map((item, i) => {
                                    return <tr key={i}>
                                        <th>{item.currency}</th>
                                        <th>{item.exchangeValueDifference}%</th>
                                        <th>{item.oldExchangeValue}</th>
                                        <th>{item.currentExchangeValue}</th>
                                    </tr>;
                                })
                            }
                        </table>
                        : null)
                }
            </div>
        );
    }
}
export default connect()(Home);
