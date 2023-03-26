window.HarGauge = {
    interop: {
        createGauge: (elementId, config) => {
            const gauge = new Gauge(elementId, config);
            gauge.render();
            window.HarGauge.gaugeContainer[elementId] = gauge;
        },
        updateGaugeValue: (elementId, value) => {
            if (window.HarGauge.gaugeContainer[elementId] === undefined) return;
            window.HarGauge.gaugeContainer[elementId].redraw(value);
        },
        updateRedZone: (elementId, from, to) => {
            if (window.HarGauge.gaugeContainer[elementId] === undefined) return;
            window.HarGauge.gaugeContainer[elementId].redrawRedZone(from, to);
        },
        updateYellowZone: (elementId, from, to) => {
            if (window.HarGauge.gaugeContainer[elementId] === undefined) return;
            window.HarGauge.gaugeContainer[elementId].redrawYellowZone(from, to);
        },
        updateGreenZone: (elementId, from, to) => {
            if (window.HarGauge.gaugeContainer[elementId] === undefined) return;
            window.HarGauge.gaugeContainer[elementId].redrawGreenZone(from, to);
        },
        destroyGauge: (elementId) => {
            if (window.HarGauge.gaugeContainer === undefined || window.HarGauge.gaugeContainer[elementId] === undefined) return;
            delete window.HarGauge.gaugeContainer[elementId];
        }
    },
    gaugeContainer: []
}