<template>
    <div :class="alignment">
        <h1 class="page-title">Contact Us</h1>
        <p class="page-description">Have questions or want to work together? Send us a message!</p>
        <form @submit.prevent="submitForm" class="contact-form">
            <input v-model="form.name" type="text" placeholder="Your Name" required />
            <input v-model="form.email" type="email" placeholder="Your Email" required />
            <input v-model="form.subject" type="text" placeholder="Subject" required />
            <textarea v-model="form.message" placeholder="Your Message" required></textarea>
            <button type="submit" class="primary-btn">Send Message</button>
        </form>
        <div v-if="responseMessage" class="response-message">{{ responseMessage }}</div>
    </div>
</template>
<script setup lang="ts">
import { ref } from 'vue';
import type { Contact } from '~/api/models/Contact';
const props = defineProps<{
    alignment: string;
}>();
const form = ref<Contact>({
    name: '',
    email: '',
    subject: '',
    message: ''
});
const responseMessage = ref('');

async function submitForm() {
    try {
        if (!validateForm()) {
            return;
        }
        const response = await fetch("/api/Contact/SendMessage", {
            method: "POST",
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            },
            body: JSON.stringify(form.value)
        });

        if (response.ok) {
            responseMessage.value = "Your message has been sent successfully!";
            form.value = { name: '', email: '', subject: '', message: '' }; // Reset form
        } else {
            responseMessage.value = "There was an error sending your message. Please try again later.";
        }
    } catch (error) {
        console.error("Error submitting contact form:", error);
        responseMessage.value = "An unexpected error occurred. Please try again later.";
    }
}

function validateForm() {
    if (!form.value.name?.trim()) {
        responseMessage.value = "Please enter your name.";
        return false;
    }
    if (!form.value.email?.trim()) {
        responseMessage.value = "Please enter your email.";
        return false;
    }
    if (!form.value.subject?.trim()) {
        responseMessage.value = "Please enter a subject.";
        return false;
    }
    if (!form.value.message?.trim()) {
        responseMessage.value = "Please enter your message.";
        return false;
    }
    return true;
}
</script>
<style scoped>
.page-title {
    font-size: 36px;
    margin-bottom: 10px;
}
.page-description {
    font-family: 'Barlow C Light';
    font-size: 18px;
    margin-bottom: 20px;
}
.contact-form {
    display: flex;
    flex-direction: column;
    gap: 15px;
    max-width: 500px;
}
.contact-form input,
.contact-form textarea {
    padding: 10px;
    font-size: 16px;
    border: 1px solid #ccc;
    border-radius: 4px;
}
.primary-btn {
    background-color: #333;
    color: #fff;
    border: none;
    padding: 10px 20px;
    font-size: 16px;
    cursor: pointer;
    border-radius: 4px;
}
.primary-btn:hover {
    background-color: #555;
}
.response-message {
    margin-top: 20px;
    font-size: 16px;
    color: green;
}
</style>