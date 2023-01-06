import axios from 'axios';
import moment from 'moment/moment';
import React, { useState } from 'react';

import { Button, Modal } from 'reactstrap';

import './Booking.css';


const customStyle = {
    backgroundColor: 'white',
    width: '500px',
    position: 'fixed',
    top: '15%',
    left: '50%',
    border: '2px solid black',
    borderRadius: '5px',
    right: 'auto',
    bottom: 'auto',
    marginRight: '-50%',
    verticalAlign: 'top',
    transform: 'translate(-50%, -50%)',
    textAlign: 'center'
}


const format = "YYYY-MM-DDTHH:mm:ss";
let currentDate = new Date().toISOString().slice(0, -8);

function roundToNearestHour(date) {
    date.setMinutes(date.getMinutes() + 30);
    date.setMinutes(0, 0, 0);
    return date;
}

function addHours(numOfHours, date) {
    date.setTime(date.getTime() + numOfHours * 60 * 60 * 1000);
    return date;
}

function addMinutes(numOfMinutes, date) {
    date.setMinutes(date.getMinutes() + numOfMinutes);
    return date;
}

function Booking(props) {
    const { isOpen, handleClose, handleResult, selectedResource } = props
    const [dateFrom, setDateFrom] = useState(addHours(2, roundToNearestHour(new Date())));
    const [dateTo, setDateTo] = useState(addMinutes(45, addHours(2, roundToNearestHour(new Date()))));
    const [quantity, setQuantity] = useState(selectedResource.quantity);


    function postBooking() {
        axios.post('api/bookings', {
            createdByUserId: 1,
            bookingData: {
                dateFrom: dateFrom,
                dateTo: dateTo,
                bookedQuantity: Number(quantity),
                resourceId: selectedResource.id
            }
        })
            .then(function (response) {
                handleResult();
            })
            .catch(function (error) {
                handleResult(error);
            });
    };

    function handleSubmit() {
        postBooking();
    }

    function handleQuantityChange(event) {
        setQuantity(event.target.value);
    }

    function handleDateFromChange(event) {
        if (!event.target['validity'].valid) return;
        const date = event.target['value'] + ':00';
        setDateFrom(date);
    }

    function handleDateToChange(event) {
        if (!event.target['validity'].valid) return;
        const date = event.target['value'] + ':00';
        setDateTo(date);
    }

    return (
        <Modal style={customStyle} isOpen={isOpen && (selectedResource !== null)} toggle={handleClose}>
            <div style={{ borderBottom: '2px solid black', padding: '10px', textAlign: 'right' }}>
                <label style={{ display: 'inline-block', width: '200px', textAlign: 'center', verticalAlign: 'top' }}>Booking {selectedResource.name}</label>
                <div style={{
                    display: 'inline-block',
                    width: '150px',
                    textAlign: 'right',
                }}>
                    <button style={{ backgroundColor: 'transparent', borderRadius: '5px' }} onClick={handleClose} type="button">
                        &times;
                    </button>
                </div>
            </div>
            <form>
                <div style={{ paddingRight: '10px' }}>
                    <div style={{ padding: '10px', verticalAlign: 'top', textAlign: 'right' }}>
                        <label style={{ display: 'inline-block', width: '200px', padding: '10px', textAlign: 'left' }}>Date From</label>
                        <input
                            type="datetime-local"
                            style={{
                                display: 'inline-block',
                                width: '150px',
                                padding: '10px',
                                textAlign: 'right',
                                borderRadius: '5px',
                                border: '2px solid black'
                            }}
                            onChange={handleDateFromChange}
                            value={moment(dateFrom).format(format)}
                            min={moment(currentDate)}
                            max={moment(dateTo).format(format)}
                            required
                            step="900"
                        >
                        </input>
                    </div>
                    <div style={{ padding: '10px', verticalAlign: 'top', textAlign: 'right' }}>
                        <label style={{ display: 'inline-block', width: '200px', padding: '10px', textAlign: 'left' }}>Date To</label>
                        <input
                            type="datetime-local"
                            style={{
                                borderRadius: '12px',
                                width: '150px',
                                padding: '10px',
                                textAlign: 'right',
                                verticalAlign: 'top'
                            }}
                            value={moment(dateTo).format(format)}
                            min={moment(dateFrom).format(format)}
                            step="900"
                            required
                            onChange={handleDateToChange}>
                        </input>
                    </div>
                    <div style={{ padding: '10px', verticalAlign: 'top', textAlign: 'right' }}>
                        <label style={{ display: 'inline-block', width: '200px', padding: '10px', textAlign: 'left' }}>Quantity</label>
                        <input style={{ borderRadius: '5px', width: '150px', padding: '10px', textAlign: 'right', verticalAlign: 'top' }}
                            type="number"
                            required
                            min={0}
                            max={selectedResource.quantity}
                            defaultValue={selectedResource.quantity}
                            onChange={handleQuantityChange}
                        />
                    </div>
                </div>
                <div style={{ padding: '10px', textAlign: 'right', verticalAlign: 'top' }}>
                    <div style={{ display: 'inline-block', padding: '10px', textAlign: 'right' }}>
                        <Button style={{ width: '100px', height: '35px', backgroundColor: 'transparent', borderRadius: '12px', border: '2px solid black' }} onClick={handleSubmit}>
                            Book
                        </Button>
                    </div>
                </div>
            </form>
        </Modal>
    );
}

export default Booking;
