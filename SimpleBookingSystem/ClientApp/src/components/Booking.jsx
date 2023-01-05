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
    const { isOpen, handleClose, selectedResource } = props
    const [error, setError] = useState(null);
    const [dateFrom, setDateFrom] = useState(addHours(2,roundToNearestHour(new Date())));
    const [dateTo, setDateTo] = useState(addMinutes(45, addHours(2,roundToNearestHour(new Date()))));
    const [quantity, setQuantity] = useState(0);

    const closeBtn = (
      <button className="close" onClick={handleClose} type="button">
        &times;
      </button>
    );

    function handleSubmit(e, resource) {
            
    }

    function handleQuantityChange(e){
        setQuantity(e.value);
    }
    
    return (
            <Modal isOpen={isOpen && selectedResource} toggle={handleClose}>
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
        );
}

export default Booking;
