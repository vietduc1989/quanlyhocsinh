// QUAN-20260530-0942
import React from 'react';
import { Modal } from 'antd';

interface ConfirmationModalProps {
  visible: boolean;
  onConfirm: () => void;
  onCancel: () => void;
  title?: string;
  content?: string | React.ReactNode;
  okText?: string;
  cancelText?: string;
  loading?: boolean;
}

const ConfirmationModal: React.FC<ConfirmationModalProps> = ({
  visible,
  onConfirm,
  onCancel,
  title = 'Xác nhận',
  content = 'Bạn có chắc chắn muốn thực hiện hành động này?',
  okText = 'Xác nhận',
  cancelText = 'Hủy',
  loading = false,
}) => {
  return (
    <Modal
      title={title}
      open={visible}
      onOk={onConfirm}
      onCancel={onCancel}
      okText={okText}
      cancelText={cancelText}
      okButtonProps={{ loading }}
    >
      {content}
    </Modal>
  );
};

export default ConfirmationModal;