// QUAN-20260530-2301
import { useState, useEffect } from 'react';
import { TextInput, Select, Button, Group, Box } from '@mantine/core';
import { IconSearch } from '@tabler/icons-react';
import { useLopsAndTrangThai } from '../hooks/useLopsAndTrangThai'; // Custom hook to fetch lookup data
import type { StudentListFilter } from '../types/student';

interface StudentFilterFormProps {
  filters: StudentListFilter;
  onFilterChange: (newFilters: Partial<StudentListFilter>) => void;
  onSearch: () => void;
  onClear: () => void;
}

export function StudentFilterForm({ filters, onFilterChange, onSearch, onClear }: StudentFilterFormProps) {
  const [searchQuery, setSearchQuery] = useState(filters.searchQuery || '');
  const [lopId, setLopId] = useState<string | null>(filters.lopId || null);
  const [trangThaiId, setTrangThaiId] = useState<string | null>(filters.trangThaiId || null);

  const { lops, trangThais, isLoadingLops, isLoadingTrangThais } = useLopsAndTrangThai();

  useEffect(() => {
    setSearchQuery(filters.searchQuery || '');
    setLopId(filters.lopId || null);
    setTrangThaiId(filters.trangThaiId || null);
  }, [filters]);

  const handleApplyFilters = () => {
    onFilterChange({
      searchQuery: searchQuery || undefined,
      lopId: lopId || undefined,
      trangThaiId: trangThaiId || undefined,
      pageNumber: 1,
    });
    onSearch();
  };

  const handleClearFilters = () => {
    setSearchQuery('');
    setLopId(null);
    setTrangThaiId(null);
    onClear();
    onFilterChange({
      searchQuery: undefined,
      lopId: undefined,
      trangThaiId: undefined,
      pageNumber: 1,
    });
  };

  return (
    <Box>
      <Group mb="md" grow>
        <TextInput
          placeholder="Tìm kiếm theo Mã HS, Họ Tên"
          icon={<IconSearch size="1rem" />}
          value={searchQuery}
          onChange={(event) => setSearchQuery(event.currentTarget.value)}
          onKeyDown={(event) => {
            if (event.key === 'Enter') handleApplyFilters();
          }}
        />
        <Select
          placeholder="Lọc theo Lớp"
          data={lops.map((lop) => ({ value: lop.id, label: lop.tenLop }))}
          value={lopId}
          onChange={setLopId}
          clearable
          disabled={isLoadingLops}
          loading={isLoadingLops}
        />
        <Select
          placeholder="Lọc theo Trạng Thái"
          data={trangThais.map((tt) => ({ value: tt.id, label: tt.tenTrangThai }))}
          value={trangThaiId}
          onChange={setTrangThaiId}
          clearable
          disabled={isLoadingTrangThais}
          loading={isLoadingTrangThais}
        />
      </Group>
      <Group position="right">
        <Button variant="default" onClick={handleClearFilters}>
          Xóa bộ lọc
        </Button>
        <Button onClick={handleApplyFilters}>
          Áp dụng
        </Button>
      </Group>
    </Box>
  );
}