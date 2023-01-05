import axios from 'axios';
import React, { useState } from 'react';
import DateTimePicker from 'react-datetime-picker';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';

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
    const [error, setError] = useState(null);
    const [dateFrom, setDateFrom] = useState(addHours(2,roundToNearestHour(new Date())));
    const [dateTo, setDateTo] = useState(addMinutes(45, addHours(2,roundToNearestHour(new Date()))));
    const [quantity, setQuantity] = useState(selectedResource.quantity);

    const closeBtn = (
      <button className="close" onClick={handleClose} type="button">
        &times;
      </button>
    );

    function postBooking() {
        axios.post('api/bookings', {
            dateFrom: dateFrom,
            dateTo: dateTo,
            bookedQuantity: Number(quantity),
            resourceId: selectedResource.id
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

    function handleQuantityChange(event){
        setQuantity(event.target.value);
    }
    
    return (
        <div>
            <Modal isOpen={isOpen && (selectedResource !== null)} toggle={handleClose}>
                <ModalHeader toggle={handleClose} close={closeBtn}>
                    Booking {selectedResource.name}
                </ModalHeader>
                <ModalBody>
                    <div>
                        <p>Date From</p>
                        <DateTimePicker disableClock={true} onChange={setDateFrom} value={dateFrom}></DateTimePicker>
                    </div>
                    <div>
                        <p>Date To</p>
                        <DateTimePicker disableClock={true} onChange={setDateTo} value={dateTo}></DateTimePicker>
                    </div>
                    <div>
                        <p>Quantity</p>
                        <input
                            type="number"
                            min={0}
                            max={selectedResource.quantity}
                            defaultValue={selectedResource.quantity} 
                            onChange={handleQuantityChange}
                        />
                    </div>
                </ModalBody>
                <ModalFooter>
                <Button onClick={handleSubmit}>
                    Book
                </Button>
                </ModalFooter>
            </Modal>
        </div>

        );
}

export default Booking;
