import { required, between, maxLength } from 'vuelidate/lib/validators';
import axios from 'axios';
import Vue from 'vue';

export default {
    name: 'app-form',
    data() {
        return {
            isSubmitted: false,
            isError: false,
            errorHeader: 'error.invalidFields',
            errors: [],
            genders: this.getGenders(),
            submitting: false,
            form: {
                firstName: '',
                lastName: '',
                dateOfBirth: null,
                terms: false,
                gender: null,
                additionalInfo: ''
            },
            result: ''
        }
    },
    methods: {
        submit() {
            this.$v.$touch();
            if (!this.$v.$error) {
                this.sendFormData();
            } else {
                this.validationError();
            }
        },
        enableSubmitLoader() {
            this.submitting = true;
        },
        disableSubmitLoader() {
            this.submitting = false;
        },
        sendFormData() {
            this.enableSubmitLoader();

            var obj = Object.assign({ name: this.form.firstName + ' ' + this.form.lastName }, this.form);

            axios.post(Vue.config.formApiUrl, obj).then(response => {
                this.submitSuccess(response);
                this.disableSubmitLoader();
            }).catch(error => {
                this.submitError(error);
                this.disableSubmitLoader();
            });
        },
        submitSuccess(response) {
            if (response.data.result) {
                this.isSubmitted = true;
                this.isError = false;
                this.result = response.data.result;
            } else {
                this.errorHeader = 'error.invalidFields';
                this.errors = response.data.errors;
                this.isError = true;
                this.result = '';
            }
        },
        submitError(error) {
            this.errorHeader = 'error.general';
            this.errors = [{ 'field': null, 'message': error.response.data.errors }];
            this.isError = true;
        },
        validationError() {
            this.errorHeader = 'error.invalidFields';
            this.errors = this.getErrors();
            this.isError = true;
        },
        isErrorField(field) {
            try {
                if (this.getValidationField(field).$error) {
                    return true;
                }
            } catch (error) { }

            return this.errors.some(el => el.field === field);
        },
        getErrors() {
            let errors = [];
            for (const field of Object.keys(this.form)) {
                try {
                    if (this.getValidationField(field).$error) {
                        errors.push({ 'field': field, 'message': null });
                    }
                } catch (error) { }
            }
            return errors;
        },
        getFieldClasses(field) {
            return { 'is-invalid': this.isErrorField(field) }
        },
        getCharactersLeft(field) {
            try {
                return this.getValidationField(field).$params.maxLength.max - this.form[field].length;
            } catch (error) {
                return 0;
            }
        },
        getGenders() {
            return [{
                value: 'male',
                label: 'form.genders.male'
            }, {
                value: 'female',
                label: 'form.genders.female'
            }];
        },
        getValidationField(field) {
            if (this.$v.form[field]) {
                return this.$v.form[field];
            }
            throw Error('No validation for field ' + field);
        },
        onFieldBlur(field) {
            try {
                this.getValidationField(field).$touch();
                if (this.getValidationField(field).$error) {
                    if (!this.errors.some(el => el.field === field)) {
                        this.errors.push({ 'field': field, 'message': null });
                    }
                } else {
                    this.errors = this.errors.filter(el => el.field !== field);
                }
            } catch (error) { }
        },
        reload() {
            window.location = '';
        }
    },
    validations: {
        form: {
            dateOfBirth: { required },
            firstName: { required },
            lastName: { required },
            gender: { required },
            terms: { required },
            additionalInfo: { maxLength: maxLength(1000) }
        }
    },
    watch: {
        errors() {
            this.isError = this.errors.length > 0 ? true : false;
        }
    }
}
