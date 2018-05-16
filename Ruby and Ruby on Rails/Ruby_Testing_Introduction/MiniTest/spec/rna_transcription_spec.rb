require 'rna_transcription'

describe '#of_dna' do
  it 'returns G(uanine) for C(ytosine)' do
    expect(Complement.of_dna('C')).to eq 'G'
  end

  it 'returns C(ytosine) for G(uanine)' do
    expect(Complement.of_dna('G')).to eq 'C'
  end

  it 'returns A(denine) for T(hymine)' do
    expect(Complement.of_dna('T')).to eq 'A'
  end

  it 'returns U(racil) for A(denine)' do
    expect(Complement.of_dna('A')).to eq 'U'
  end

  it 'returns UGCACCAGAAUU for ACGTGGTCTTAA' do
    expect(Complement.of_dna('ACGTGGTCTTAA')).to eq 'UGCACCAGAAUU'
  end

  it 'returns "" for incorrect string' do
    expect(Complement.of_dna('XYZ')).to eq ''
  end

  it 'returns "" for partially incorrect string' do
    expect(Complement.of_dna('ACGTXYZ')).to eq ''
  end
end
